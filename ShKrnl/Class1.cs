namespace ShKrnl
{
    public class Api
    {
        /// <summary>
        /// Start a new process
        /// </summary>
        /// <param name="executablePath">The path to the process to start (EXE)</param>
        /// <returns></returns>
        public static int OpenNewProgram(string executablePath)
        {
            if (executablePath == null || !File.Exists(executablePath))
            {
                return 0;
            }
            File.AppendAllLines(Path.GetTempPath() + "\\opnprgs.bin", new List<string> { executablePath });
            return 1;
        }
        /// <summary>
        /// Terminate a process
        /// </summary>
        /// <param name="executablePath">The path to the process to close (EXE)</param>
        /// <returns></returns>
        public static int CloseProgram(string executablePath)
        {
            File.WriteAllText(Path.GetTempPath() + "\\opnprgs.bin", File.ReadAllText(Path.GetTempPath() + "\\opnprgs.bin").Replace(executablePath + Environment.NewLine, "").Replace(executablePath, ""));
            return 1;
        }
        public static string[] GetOpenPrograms()
        {
            return File.ReadAllLines(Path.GetTempPath() + "\\opnprgs.bin");
        }
        /// <summary>
        /// Does nothing currently
        /// </summary>
        /// <returns></returns>
        public static int ExitSh()
        {
            return 1;
        }

    }
}