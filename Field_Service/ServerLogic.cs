using System;
using System.Collections.Generic;
using NLog;
using System.Threading;

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

        //variables for limits
        const int growthLimit = 10;
        const int pollinateLimit = 10;

        // var myList = new List<bool>();

        //bool variables for current flower field statistics logging
        bool hasBlossomed = false;
        bool hasMatured = false;
        bool isPlanting = false;

        List<bool> Suns = new List<bool>();
        List<bool> Winds = new List<bool>();


        const int maxShine = 6;


        const int maxWind = 6;
        int sumShine = 0;
        int amountBlows = 0;


        public int ResgisterSun()
        {
            if (!isPlanting && !hasBlossomed)
            {


                Suns.Add(false);
                return Suns.Count() - 1;
            }
            else
            {
                return 0;
            }


        }

        public int ResgisterWind()
        {
            if (!isPlanting && hasBlossomed && !hasMatured)
            {

                Winds.Add(false);
                return Winds.Count() - 1;
            }
            else
            {
                return 0;
            }


        }

        public int Usable()
        {
            if (!isPlanting && !hasBlossomed)
            {
                //if the field blossoms, it is not growing


                return Suns.Select(it =>
                          {
                              if (it == true)
                              {
                                  return 1;
                              }
                              return 0;
                          }).Sum();

            }
            else if (!isPlanting && hasBlossomed && !hasMatured)
            {
                return Winds.Select(it =>
                    {
                        if (it == true)
                        {
                            return 1;
                        }
                        return 0;
                    }).Sum();

            }
            else
            {
                return 0;
            }
        }

        public int CheckSum()
        {
            if (!isPlanting && !hasBlossomed)
            {
                var numSunShine = Usable();
                var random = new Random();
                int rndGrowsAlittle = random.Next(0, 2);
                int rndRedcesAlittle = random.Next(-1, 1);

                if (numSunShine > Suns.Count() / 2)
                {
                    return rndGrowsAlittle;


                }
                else
                {

                    return rndRedcesAlittle;

                }
            }
            else if (!isPlanting && hasBlossomed && !hasMatured)
            {
                var numWindBlow = Usable();
                var random = new Random();
                int rndBlow = random.Next(0, 2);
                //int unrndBlow = random.Next(-1,1);

                if (numWindBlow > Winds.Count() / 2)
                {

                    return rndBlow;

                }
                else
                {

                    return 0;

                }

            }
            else
            {

                return 0;

            }
        }


        public void SetSun(int index, bool value)
        {
            if (!isPlanting)
            {
                if (!hasBlossomed) //if the field blossoms, it is not growing
                {

                    Suns[index] = value;
                    var numSunShine = Usable();
                    int progress = CheckSum();
                    sumShine = sumShine += progress;

                    if (sumShine < 0)
                    {

                        sumShine = 0;

                        log.Info("Growing-state =" + progress + "  #Current growth amount = " + sumShine + "  Shining-state  " + numSunShine + " Total-signal " + Suns.Count());

                    }
                    else
                    {


                        log.Info("Growing-state = " + progress + "  #Current growth amount = " + sumShine + "  Shining-state  " + numSunShine + " Total-signal " + Suns.Count());
                    }


                    if (sumShine >= maxShine)
                    {
                        hasBlossomed = true;

                        log.Info("The Grow-limit: " + maxWind + " was reached ");

                    }



                }
            }
        }



        public void SetWind(int index_Wind, bool value_Wind)
        {
            if (!isPlanting)
            {
                if (hasBlossomed) //if the field blossoms, it is not growing
                {
                    if (!hasMatured)
                    {

                        Winds[index_Wind] = value_Wind;
                        var numBlow = Usable();
                        //  var random = new Random();
                        int progress = CheckSum();

                        amountBlows = amountBlows += progress;
                        // sumBlow=  blow+=numBlow;

                        if (amountBlows < 0)
                        {
                            amountBlows = 0;
                            log.Info("Blowing-state = " + progress + "  #Current growth amount = " + amountBlows + " Blowed-state " + numBlow + " Total-signal " + Winds.Count());
                        }
                        else
                        {


                            log.Info("Blowing-state = " + progress + "  #Current growth amount = " + amountBlows + " Blowed-state " + numBlow + " Total-signal " + Winds.Count());
                        }

                        if (amountBlows >= maxWind)
                        {
                            hasMatured = true;

                            log.Info("The pollinate-limit: " + maxWind + " was reached ");

                        }
                    }
                }
            }
        }


        public bool Reset()
        {
            if (hasBlossomed && hasMatured)
            {
                isPlanting = true;
                hasBlossomed = false;
                hasMatured = false;
                sumShine = 0;
                amountBlows = 0;


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