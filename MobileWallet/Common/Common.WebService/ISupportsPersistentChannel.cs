namespace Common.WebService
{
    /// <summary>Represents an object that can open and close persistent channels.</summary>
    public interface ISupportsPersistentChannel
    {
        /// <summary>Closes the channel.</summary>
        void Close ();

        /// <summary>Opens the channel.</summary>
        void Open ();
    }
}
