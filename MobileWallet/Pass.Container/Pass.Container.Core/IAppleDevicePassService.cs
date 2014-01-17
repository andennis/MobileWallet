namespace Pass.Container.Core
{
    public interface IAppleDevicePassService 
	{
		void RegisterPass();
		void UnregisterPass();
		void GetPass();
        void GetChangedPassIds();
	}
}

