using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntraserviceTasks.CrossCuttingConcern;
using IntraserviceTasks.Res;

namespace IntraserviceTasks.BL.WorkingTimeCalculation
{
    class RestTimeCalc
    {
        private DateTime _start;
        private DateTime _finish;

        public RestTimeCalc(DateTime startOfPeriod, DateTime finishOfPeriod)
        {
            _start  = startOfPeriod;
            _finish = finishOfPeriod;
        }

        public TimeSpan GetRestTimeInPeriod()
        {
            CheckSequenceOfArguments();

            if (_start.Date == _finish.Date)
                return new RestTimeInOneDayCalc(this).GetInterval();

            return new RestTimeInSeveralDaysCalc(this).GetInterval();
        }

        private void CheckSequenceOfArguments()
        {
            if (_start > _finish)
                throw new Exception(Exceptions.REST_TIME_CALC_WRONG_SEQUENCE);
        }

        class RestTimeInOneDayCalc
        {
            RestTimeCalc _parent;

            public RestTimeInOneDayCalc(RestTimeCalc parent)
            {
                _parent = parent;
            }

            public TimeSpan GetInterval()
            {
                if (TheWholeDayIsRestDay()
                    || ThereIsOnlyRestTimeBetweenStartAndFinish())
                    return GetStraightInterval();

                return GetIntervalWhenWorkingTimeInside();
            }

            private bool TheWholeDayIsRestDay()
            {
                var productionCalendar = ExternalSystems.ProductionCalendar.Get();
                return productionCalendar.IsRestDay(_parent._start);
            }

            private bool ThereIsOnlyRestTimeBetweenStartAndFinish()
            {
                bool result =
                    (_parent._start.Hour < Constants.WORK_TIME_START_OF_DAY
                    && _parent._finish.Hour < Constants.WORK_TIME_START_OF_DAY)
                    ||
                    (_parent._start.Hour >= Constants.WORK_TIME_FINISH_OF_DAY
                    && _parent._finish.Hour >= Constants.WORK_TIME_FINISH_OF_DAY);

                return result;
            }

            private TimeSpan GetStraightInterval()
            {
                return _parent._finish - _parent._start;
            }

            private TimeSpan GetIntervalWhenWorkingTimeInside()
            {
                var timeBeforeWorkingDay = GetIntervalBeforeStartOfWorkingDay();
                var timeAfterWorkingDay = GetIntervalAfterEndOrWorkingDay();

                return timeBeforeWorkingDay + timeAfterWorkingDay;
            }

            private TimeSpan GetIntervalBeforeStartOfWorkingDay()
            {
                if (_parent._start.Hour >= Constants.WORK_TIME_START_OF_DAY)
                    return new TimeSpan();
                else
                    return new DateTime(_parent._start.Year, _parent._start.Month, _parent._start.Day,
                                        Constants.WORK_TIME_START_OF_DAY, 0, 0)
                                        - _parent._start;
            }

            private TimeSpan GetIntervalAfterEndOrWorkingDay()
            {
                if (_parent._finish.Hour < Constants.WORK_TIME_FINISH_OF_DAY)
                    return new TimeSpan();
                else
                    return _parent._finish -
                        (new DateTime(
                            _parent._start.Year, _parent._start.Month, _parent._start.Day,
                            Constants.WORK_TIME_FINISH_OF_DAY, 0, 0));
            }
        }

        class RestTimeInSeveralDaysCalc
        {
            private RestTimeCalc _parent;
            private ProductionCalendar _productionCalendar;
            private DateTime _processedDay;

            public RestTimeInSeveralDaysCalc(RestTimeCalc parent)
            {
                _parent = parent;
                _productionCalendar = ExternalSystems.ProductionCalendar.Get();
            }

            public TimeSpan GetInterval()
            {
                TimeSpan restTime;
                
                InitFirstProcessedDay();
                
                restTime = GetRestTimeBeforeMidnight();
                restTime += GetRestTimeOfDaysBetweenStartAndFinish();
                restTime += GetRestTimeBeforeFinish();

                return restTime;
            }

            private void InitFirstProcessedDay()
            {
                _processedDay = _parent._start.AddDays(1);
            }

            private TimeSpan GetRestTimeBeforeMidnight()
            {
                DateTime midnight = _processedDay.Date;
                if (_parent._start.Hour > Constants.WORK_TIME_FINISH_OF_DAY)
                {
                    return midnight - _parent._start;
                }
                else if (_parent._start.Hour > Constants.WORK_TIME_START_OF_DAY)
                {
                    return new TimeSpan(24 - Constants.WORK_TIME_FINISH_OF_DAY, 0, 0);
                }
                else
                {
                    return new TimeSpan(24 - Constants.WORK_TIME_FINISH_OF_DAY, 0, 0) +
                                (new TimeSpan(Constants.WORK_TIME_START_OF_DAY, 0, 0) - _parent._start.TimeOfDay);
                }
            }

            private TimeSpan GetRestTimeOfDaysBetweenStartAndFinish()
            {
                TimeSpan restTime = new TimeSpan();
                while (_processedDay.Date != _parent._finish.Date)
                {
                    restTime += GetRestTimeInDay(_processedDay);
                    _processedDay = _processedDay.AddDays(1);
                }

                return restTime;
            }

            private TimeSpan GetRestTimeInDay(DateTime day)
            {
                if (_productionCalendar.IsRestDay(day))
                    return new TimeSpan(24, 0, 0);

                int restHours = (24 - Constants.WORK_TIME_FINISH_OF_DAY) + Constants.WORK_TIME_START_OF_DAY;
                return new TimeSpan(restHours, 0, 0);
            }

            private TimeSpan GetRestTimeBeforeFinish()
            {
                if (_parent._finish.Hour < Constants.WORK_TIME_START_OF_DAY)
                {
                    return _parent._finish.TimeOfDay;
                }
                else if (_parent._finish.Hour < Constants.WORK_TIME_FINISH_OF_DAY)
                {
                    return new TimeSpan(Constants.WORK_TIME_START_OF_DAY, 0, 0);
                }
                else
                {
                    return new TimeSpan(Constants.WORK_TIME_START_OF_DAY, 0, 0) +
                                    _parent._finish.TimeOfDay - new TimeSpan(Constants.WORK_TIME_FINISH_OF_DAY, 0, 0);
                }
            }
        }
    }
}
