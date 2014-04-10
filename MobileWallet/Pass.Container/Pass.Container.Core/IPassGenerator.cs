namespace Pass.Container.Core
{
    public interface IPassGenerator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="passId"></param>
        /// <returns>File path to pass package</returns>
        string GeneratePass(int passId);
    }
}