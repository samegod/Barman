namespace Pool
{
	public interface IPoolObject
	{
		int PreloadCount { get; }
		IPoolObject Origin { get; }
		IPoolObject LoadObject(IPoolObject origin);
		void OnPop();
		void OnPush();
	}
}