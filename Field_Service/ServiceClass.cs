using System;

using Contract;


namespace Server
{
	/// <summary>
	/// Service.
	/// </summary>
	public class Service : MarshalByRefObject, IService
	{
		/// <summary>
		/// Access lock.
		/// </summary>
		private readonly Object accessLock = new Object();

		/// <summary>
		/// Service logic.
		/// </summary>
		private static ServiceLogic logic = new ServiceLogic();


		/// <summary>
		/// Add sun shining value to sun shining amount
		/// </summary>
		public void SunAdd()
		{
			lock( accessLock )
			{
				logic.SunAdd();
			}
		}

        /// <summary>
        /// Remove sun shining value from sun 
        /// </summary>
        public void SunCount()
        {
            lock (accessLock)
            {
                logic.SunCount();
            }
        }

        public void CountWind(){
            lock (accessLock)
            {
                logic.CountWind();
            }
        }

        /// <summary>
        /// Add wind blowing value to wind blowing amount
        /// </summary>
        public void WindAdd()
        {
            lock (accessLock)
            {
                logic.WindAdd();
            }
        }

        /// <summary>
        /// Reset all flower field values
        /// </summary>
        /// <returns>bool value if all values were reset successfully</returns>
        public static bool Reset()
        {
            return logic.Reset();
        }

        /// <summary>
        /// Finish to plant flower field
        /// </summary>
        public static void FinishedPlanting()
        {
            logic.FinishedPlanting();
        }

    }
}