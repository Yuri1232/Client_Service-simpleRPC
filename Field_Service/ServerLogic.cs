using System;
using System.Collections.Generic;
using NLog;

using Contract;

namespace Server
{
	/// <summary>
	/// Networking independant service logic.
	/// </summary>
	public class ServiceLogic : IService
	{
		/// <summary>
		/// Logger for this class.
		/// </summary>
		Logger log = LogManager.GetCurrentClassLogger();

        //amount variables for current flower field statistics logging
        int growthAmount;
        int pollinateAmount;

        //variables for limits
        const int growthLimit = 10;
        const int pollinateLimit = 10;

        // var myList = new List<bool>();

        //bool variables for current flower field statistics logging
        bool hasBlossomed = false;
        bool hasMatured = false;
        bool isPlanting = false;
      List <bool> myList = new List<bool>(){false};
      List <bool> myListWind = new List<bool>(){false};
      int count =0;
      int countWind = 0;


      

        /// <summary>
        /// Add sun shining value to sun shining amount
        /// </summary>
		public void SunAdd()
		{
            if (!isPlanting)
            {
                if (!hasBlossomed) //if the field blossoms, it is not growing
                {
                        myList.Add(true);
                       
                        if(count < 5){
                          var rand= new Random();
                          int grow = rand.Next(1,2);
                          growthAmount += grow;
 
                         log.Info("Growing state = +" + grow + "  (Current growth amount = " + growthAmount + ")");
                    }
                }
            }
		}

        // <summary>
        // Remove sun shining value from sun shining amount
        // </summary>
        public void SunCount()
        {
       
            if (!isPlanting)
            {
                if (!hasBlossomed) //if the field blossoms, it is not receding
                {
                       bool last = myList[myList.Count - 1];
                        if(last == true){
                            count ++;
                        }  
                      if(count > 5){
                         log.Info("Growing state is more than half");
                         var rand= new Random();
                         int Min_grow = rand.Next(0,2);
                
                        if((growthAmount + Min_grow) > growthLimit){
                             growthAmount = growthLimit;

                        }else{
                            growthAmount = growthAmount + Min_grow;

                        }
                        
                    // }
                    log.Info("Growing state = +" +  Min_grow  + "  (Current growth amount = " + growthAmount + ")");
                      if (growthAmount >= growthLimit) //when growth amount goes above chosen limit, a field blossoms
                    {
                        hasBlossomed = true;
                        log.Info(" The growth limit " + growthLimit + " was reached");
                    }
                    }
                     myList.Add(false);
                }
            }
        }

       // <summary>
        // Add wind blowing value to wind blowing amount
        // </summary>
        public void WindAdd()
        {
            if (!isPlanting)
            {
                if (!hasMatured)
                {
                    if (hasBlossomed)
                    {
                        myListWind.Add(true);
                        var random = new Random();
                        int pollinate = random.Next(1, 2);

                        if(countWind < 5){
                    
                            pollinateAmount += pollinate;
                    
                        log.Info("Pollinate state= " + pollinate + "  Current pollinate amount = " + pollinateAmount);

                        if (pollinateAmount >= pollinateLimit) //when pollinate amount goes above chosen limit, a field matures
                        {
                            hasMatured = true;
                            log.Info("The pollinate limit: +" + pollinateLimit + " was reached ");

                        }
                        }
                    }
                }
            }
        }

            public void CountWind(){

                if (!isPlanting){
                    
                    if(!hasMatured){

                        if(hasBlossomed){
                        var random = new Random();
                        int polinate = random.Next(0,2);
                            
                        bool last = myListWind[myListWind.Count - 1];

                        if(last == true)
                        {
                            countWind++;
                        }
                            if(countWind > 5)
                            {
                                 log.Info("Pollinate state is more than half");
                                 pollinateAmount += polinate;

                            
                              log.Info("Pollinate state = +" + polinate + "  Current pollinate amount = " + pollinateAmount);

                        if (pollinateAmount >= pollinateLimit) //when pollinate amount goes above chosen limit, a field matures
                        {
                            hasMatured = true;
                            log.Info("The pollinate limit " + pollinateLimit + " was reached ");

                        }

                        }
                            myListWind.Add(false);


                        }
                    }
                }
                
            
        }

        /// <summary>
        /// Reset all flower field values
        /// </summary>
        /// <returns>bool value if all values were reset successfully</returns>
        public bool Reset()
        {
            if (hasBlossomed && hasMatured)
            {
                isPlanting = true;
                hasBlossomed = false;
                hasMatured = false;
                growthAmount = 0;
                pollinateAmount = 0;
                count = 0;
                countWind = 0;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Finish to plant flower field
        /// </summary>
        public void FinishedPlanting()
        {
            isPlanting = false;
        }

    }
}