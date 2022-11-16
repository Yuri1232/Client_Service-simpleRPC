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
		public void SetSun(int index, bool value)
		{
			lock( accessLock )
			{
				logic.SetSun(index,value);
			}
		}

    
        public int ResgisterSun( ){
            lock( accessLock )
			{

            return logic.ResgisterSun();
            }

          }

          	public void SetWind(int index,bool windValue)
		{
			lock( accessLock )
			{
				logic.SetWind(index,windValue);
			}
		}

    
        public int ResgisterWind(){
            lock( accessLock )
			{

            return logic.ResgisterWind();
            }

          }

		   public int CheckSum(){
            lock( accessLock )
			{
              return logic.CheckSum();
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

		public int Usable(){
			return logic.Usable();
		}

    }
}