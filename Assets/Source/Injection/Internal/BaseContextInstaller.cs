using Zenject;


namespace StudioEntropy.Injection.Internal
{

    internal abstract class BaseContextInstaller
    {
        /// <summary>
        /// Executes this installer on the specified <see cref="Context"/>.
        /// </summary>
        /// <param name="context"></param>
        public abstract void Install( Context context );

    }

}
