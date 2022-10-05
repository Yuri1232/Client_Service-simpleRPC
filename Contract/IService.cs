using System;

namespace Contract 
{
	/// <summary>
	/// Service contract.
	/// </summary>
	public interface IService
	{
        /// <summary>
        /// Add sun shining value to sun shining amount
        /// </summary>
        void SunAdd();

        /// <summary>
        /// Remove sun shining value from sun 
        /// </summary>
        void SunCount();

        /// <summary>
        /// Add wind blowing value to wind blowing amount
        /// </summary>
        void WindAdd();


        void CountWind();

    }
}
