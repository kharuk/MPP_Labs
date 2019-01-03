using System;

namespace Injector
{

    public class TypeModel
    {
        public Type Type { get; }

        /// <summary>
        /// Store singleton object
        /// </summary>
        /// <remarks>
        /// If TimeToLoive is InstancePerDependency, it equals null
        /// </remarks>
        public object Instance { get; set; }

        /// <summary>
        /// Store the way of getting object
        /// </summary>
        public TimeToLive TimeToLive { get; }

        public TypeModel(Type type, TimeToLive timeToLive)
        {
            Type = type;
            Instance = null;
            TimeToLive = timeToLive;
        }
    }
}
