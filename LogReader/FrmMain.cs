using LC.MES.DLLService;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogReader
{
    public partial class FrmMain : Form
    {
        private IniFile _ini;
        private readonly MESHelper _mes;
        private string rMsg;
        private readonly Cache _Cache;
        private string _sourcePath, _destPath, _topUser, _botUser, _Tpwd, _Bpwd, _topRes, _botRes, _time = "3000";

        public FrmMain()
        {
            InitializeComponent();
            _mes = new MESHelper();
            _Cache = new Cache();
            this.Load += Form1_LoadAsync;
            timer.Tick += Timer1_TickAsync;
        }

        private async void Form1_LoadAsync(object sender, EventArgs e)
        {
            try
            {
                string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.ini");
                _ini = new IniFile(configPath);
                await AppLogger.LogAsync("Configuration file loaded successfully.");

                _sourcePath = _ini.Read("Paths", "SourcePath");
                _destPath = _ini.Read("Paths", "DestPath");

                _topUser = _ini.Read("General", "TOPUSERID");
                _botUser = _ini.Read("General", "BOTTOMUSERID");

                _Tpwd = _ini.Read("General", "TOPUSERPass");
                _Bpwd = _ini.Read("General", "BOTUSERPass");

                _topRes = _ini.Read("Resources", "TopResources");
                _botRes = _ini.Read("Resources", "BottomResources");

                _time = _ini.Read("Time", "Interval");

                txtSourcePath.Text = _sourcePath;
                txtDestPath.Text = _destPath;
                txtTopUser.Text = _topUser;
                txtBotUser.Text = _botUser;
                txtPassTop.Text = _Tpwd;
                txtPassBot.Text = _Bpwd;
                txtTopRes.Text = _topRes;
                txtBotRes.Text = _botRes;
                
                if (!int.TryParse(_time, out int interval))
                {
                    interval = 3000;
                    await AppLogger.LogAsync($"Invalid timer interval '{_time}', using default 3000ms.");
                }
                timer.Interval = interval;

                try
                {
                    if (!await Task.Run(() => _mes.CheckUserAndResourcePassed(_topUser, _topRes, _Tpwd, out rMsg)))
                    {
                        await AppLogger.LogAsync($"FAILED: Top User/Resource check failed. User={_topUser}, Resource={_topRes}, Error={rMsg}");
                        MessageBox.Show("Invalid Top User, Resource, or Password. Please check your credentials.", "Authentication Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtMsg.Text = rMsg;
                        return;
                    }

                    if (!await Task.Run(() => _mes.CheckUserAndResourcePassed(_botUser, _botRes, _Bpwd, out rMsg)))
                    {
                        await AppLogger.LogAsync($"FAILED: Bottom User/Resource check failed. User={_botUser}, Resource={_botRes}, Error={rMsg}");
                        MessageBox.Show("Invalid Bottom User, Resource, or Password. Please check your credentials.", "Authentication Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtMsg.Text = rMsg;
                        return;
                    }
                    await AppLogger.LogAsync($"SUCCESS: User '{_topUser}' authenticated for resource '{_topRes}' & User '{_botUser}' authenticated for resource '{_botRes}'");

                    timer.Start();
                    await AppLogger.LogAsync($"Application started successfully. Timer interval set to {timer.Interval}ms");
                }
                catch (Exception ex)
                {
                    await AppLogger.LogAsync($"ERROR during authentication: {ex.GetType().Name} - {ex.Message}\nStackTrace: {ex.StackTrace}");
                    MessageBox.Show($"Error during authentication: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                await AppLogger.LogAsync($"ERROR during form load: {ex.GetType().Name} - {ex.Message}\nStackTrace: {ex.StackTrace}");
                MessageBox.Show($"Error loading configuration: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void Timer1_TickAsync(object sender, EventArgs e)
        {
            try
            {
                timer.Stop();
                await AppLogger.LogAsync("Timer tick: Starting file processing cycle.");
                await ProcessFilesAsync();
                txtTime.Text = "Last check: " + DateTime.Now;
                timer.Start();
            }
            catch (Exception ex)
            {
                await AppLogger.LogAsync($"ERROR in Timer1_TickAsync: {ex.GetType().Name} - {ex.Message}\nStackTrace: {ex.StackTrace}");
                timer.Start();
            }
        }

        private async Task ProcessFilesAsync()
        {
            try
            {
                if (!Directory.Exists(_sourcePath))
                {
                    await AppLogger.LogAsync($"WARNING: Source path does not exist: {_sourcePath}");
                    return;
                }
                
                var directoriesToCheck = new[]
                {
                      _sourcePath,
                      Path.Combine(_sourcePath, "PASS"),
                      Path.Combine(_sourcePath, "NG")
                 };

                foreach (var dir in directoriesToCheck)
                {
                    try
                    {
                        if (!Directory.Exists(dir)) continue;

                        var files = Directory.GetFiles(dir).OrderBy(f => new FileInfo(f).LastWriteTime);
                        if (files.Any())
                            await AppLogger.LogAsync($"Processing {files.Count()} files from {dir}");
                        
                        var tasks = files.Select(file => ProcessLatestFileAsync(file));
                        await Task.WhenAll(tasks);
                    }
                    catch (Exception ex)
                    {
                        await AppLogger.LogAsync($"ERROR processing directory '{dir}': {ex.GetType().Name} - {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                await AppLogger.LogAsync($"ERROR in ProcessFilesAsync: {ex.GetType().Name} - {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
        }

        private async Task ProcessLatestFileAsync(string file)
        {
            try
            {
                if (file == null) return;

                // Wait for file to be unlocked
                int waitAttempts = 0;
                while (IsFileLocked(file) && waitAttempts < 10)
                {
                    await Task.Delay(500);
                    waitAttempts++;
                }
                
                if (waitAttempts >= 10)
                {
                    await AppLogger.LogAsync($"WARNING: File locked after 10 attempts, skipping: {Path.GetFileName(file)}");
                    return;
                }

                string psn = null, result = null, stage = "Not Found";

                try
                {
                    foreach (var line in File.ReadAllLines(file))
                    {
                        var u = line.ToUpperInvariant();
                        if (u.Contains("BARCODE"))
                            psn = line.Replace("Barcode,", "").Trim().ToUpper();
                        else if (u.Contains("RESULT"))
                            result = line.Replace("Result,", "").Trim().ToUpper();
                        else if (u.Contains("_T"))
                        {
                            stage = "TOP";
                            break;
                        }
                        else if (u.Contains("_B"))
                        {
                            stage = "BOT";
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    await AppLogger.LogAsync($"ERROR reading file '{Path.GetFileName(file)}': {ex.GetType().Name} - {ex.Message}");
                    await MoveToArchiveAsync(file, stage, "ERROR");
                    return;
                }
                
                if (result != "NG") result = "OK";

                if (string.IsNullOrEmpty(psn))
                {
                    await AppLogger.LogAsync($"ERROR: [{Path.GetFileName(file)}] PSN not found → file moved to archive (stage={stage}).");
                    await MoveToArchiveAsync(file, stage, "ERROR");
                    return;
                }
                
                string st = $"{psn}|{result}";
                {
                    if (!_Cache.TryMarkAndRun(st))
                    {
                        await AppLogger.LogAsync($"INFO: {psn}|{result} - Already processed, skipped and archived.");
                        await MoveToArchiveAsync(file, stage, result);
                        return;
                    }
                }

                string user, res, _pwd;
                if (stage == "TOP")
                {
                    user = _topUser;
                    res = _topRes;
                    _pwd = _Tpwd;
                }
                else
                {
                    user = _botUser;
                    res = _botRes;
                    _pwd = _Bpwd;
                }

                await AppLogger.LogAsync($"INFO: [{Path.GetFileName(file)}] Starting processing - PSN={psn}, Stage={stage}, User={user}, Resource={res}");

                try
                {
                    DataTable dt = await Task.Run(() => _mes.GetSnByPsnOrSn(psn));

                    if (dt == null || dt.Rows.Count == 0)
                    {
                        await AppLogger.LogAsync($"ERROR: [{Path.GetFileName(file)}] SN lookup failed for PSN={psn} - No data returned from MES");
                        txtMsg.Text = "SN not found for PSN " + psn;
                        await MoveToArchiveAsync(file, stage, "ERROR");
                        return;
                    }
                    
                    var snList = dt.Rows.Cast<DataRow>()
                                                               .Select(r => r["SN"].ToString())
                                                               .ToList();
                    await AppLogger.LogAsync($"INFO: Found {snList.Count} SNs for PSN={psn}");

                    var tasks = snList.Select(async sn =>
                    {
                        try
                        {
                            string rMsg = "";

                            bool routePassed = await Task.Run(() => _mes.CheckRoutePassed(sn, res, out rMsg));

                            if (!routePassed)
                            {
                                await AppLogger.LogAsync($"WARNING: [{Path.GetFileName(file)}] Route check failed for SN={sn}, Resource={res}. Error={rMsg}");
                                return;
                            }

                            string error = result.Equals("NG", StringComparison.OrdinalIgnoreCase) ? "Insufficient" : "";
                            string errMsg = "";
                            await Task.Run(() => _mes.SetMobileData(sn, res, user, result, error, out errMsg));
                            await AppLogger.LogAsync($"SUCCESS: [{Path.GetFileName(file)}] Posted - PSN={psn}, SN={sn}, Result={result}");
                        }
                        catch (Exception ex)
                        {
                            await AppLogger.LogAsync($"ERROR processing SN {sn}: {ex.GetType().Name} - {ex.Message}");
                        }
                    });

                    await Task.WhenAll(tasks);

                    txtMsg.Text = $"Posted {psn} : {result}";
                    await MoveToArchiveAsync(file, stage, result);
                }
                catch (Exception ex)
                {
                    await AppLogger.LogAsync($"ERROR: [{Path.GetFileName(file)}] Processing failed - {ex.GetType().Name}: {ex.Message}\nStackTrace: {ex.StackTrace}");
                    txtMsg.Text = "Error during processing, see logs.";
                    await MoveToArchiveAsync(file, stage, "ERROR");
                }
            }
            catch (Exception ex)
            {
                await AppLogger.LogAsync($"ERROR in ProcessLatestFileAsync: {ex.GetType().Name} - {ex.Message}\nStackTrace: {ex.StackTrace}");

        private async Task MoveToArchiveAsync(string sourceFile, string stage, string status)
        {
            try
            {
                string dayFolder = Path.Combine(_destPath, DateTime.Now.ToString("ddMMMyyyy"), stage, status);
                
                try
                {
                    if (!Directory.Exists(dayFolder))
                        Directory.CreateDirectory(dayFolder);
                }
                catch (Exception ex)
                {
                    await AppLogger.LogAsync($"ERROR: Failed to create archive directory '{dayFolder}': {ex.GetType().Name} - {ex.Message}");
                    return;
                }

                string dest = Path.Combine(dayFolder, Path.GetFileName(sourceFile));

                if (File.Exists(dest))
                {
                    try
                    {
                        string directory = Path.GetDirectoryName(dest);
                        string filenameWithoutExt = Path.GetFileNameWithoutExtension(dest);
                        string extension = Path.GetExtension(dest);

                        int counter = 1;
                        string newPath;
                        do
                        {
                            newPath = Path.Combine(directory, $"{filenameWithoutExt}_{counter}{extension}");
                            counter++;
                        }
                        while (File.Exists(newPath) && counter < 1000); // Safety limit

                        if (counter >= 1000)
                        {
                            await AppLogger.LogAsync($"WARNING: Too many duplicate files for '{Path.GetFileName(sourceFile)}', skipping rename.");
                            return;
                        }

                        File.Move(dest, newPath);
                        await AppLogger.LogAsync($"INFO: Renamed existing archive file to {Path.GetFileName(newPath)}");
                    }
                    catch (Exception ex)
                    {
                        await AppLogger.LogAsync($"ERROR: Failed to rename existing archive file: {ex.GetType().Name} - {ex.Message}");
                        return;
                    }
                }

                try
                {
                    await Task.Run(() => File.Move(sourceFile, dest));
                    await AppLogger.LogAsync($"INFO: File archived successfully: {Path.GetFileName(sourceFile)} → {stage}/{status}");
                }
                catch (Exception ex)
                {
                    await AppLogger.LogAsync($"ERROR: Failed to move file '{Path.GetFileName(sourceFile)}' to archive: {ex.GetType().Name} - {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                await AppLogger.LogAsync($"ERROR in MoveToArchiveAsync: {ex.GetType().Name} - {ex.Message}");
            }
        }

        private bool IsFileLocked(string path)
        {
            FileStream stream = null;
            try
            {
                stream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                return false;
            }
            catch (IOException ex)
            {
                // File is locked - this is expected behavior
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Unexpected error checking file lock status for '{path}': {ex.Message}");
                return true; // Assume locked on error
            }
            finally
            {
                stream?.Close();
                stream?.Dispose();
            }
        }
    }
}
