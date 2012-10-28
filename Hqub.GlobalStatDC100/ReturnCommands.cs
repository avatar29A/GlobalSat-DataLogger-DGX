namespace Hqub.GlobalSat
{
    public enum Commands
    {
        /// <summary>
        /// Get Configuration - 0xB7
        /// </summary>
        GetConfiguration = 0xB7,

        /// <summary>
        /// Set Configuration - 0xB8
        /// </summary>
        SetConfiguration = 0xB8,

        /// <summary>
        /// Get Track File Header - 0xBB
        /// </summary>
        GetTrackFileHeader = 0xBB,

        /// <summary>
        /// Get Track File - 0xB5
        /// </summary>
        GetGpsRecs = 0xB5,

        /// <summary>
        /// Delete All Track Files - 0xBA
        /// </summary>
        DeleteAllTrackFiles = 0xBA,

        /// <summary>
        /// Get The ID Of DG - 0xBF
        /// </summary>
        GetTheIdOfDg = 0xBF,
        /// <summary>
        /// Set The ID Of GD - 0xC0
        /// </summary>
        SetTheIdOfDg = 0xC0,
        /// <summary>
        /// Mouse Mode - 0xBC
        /// </summary>
        MouseMode = 0xBC
    }
}
