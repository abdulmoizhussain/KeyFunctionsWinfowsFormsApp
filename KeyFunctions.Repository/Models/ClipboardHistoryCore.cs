using KeyFunctions.Common.Enums;
using KeyFunctions.Common.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyFunctions.Repository.Models
{
    public class ClipboardHistoryCore
    {
        public int Id { get; private set; }
        public string ClipData { get; set; }

        private int _clipDataTypeInt;
        public int ClipDataTypeInt
        {
            get => _clipDataTypeInt;
            set
            {
                _clipDataTypeInt = value;
                _clipDataType = (ClipDataType)_clipDataTypeInt;
            }
        }

        private ClipDataType _clipDataType;
        public ClipDataType ClipDataType
        {
            get => _clipDataType;
            set
            {
                _clipDataType = value;
                _clipDataTypeInt = (int)_clipDataType;
            }
        }

        private DateTime _dateTime;
        public DateTime DateTime
        {
            get => _dateTime;
            set
            {
                _dateTime = value;
                _dateTimeStamp = _dateTime.ToString(Globals.DateTimeStringFormat);
            }
        }

        public string _dateTimeStamp;
        public string DateTimeStamp
        {
            get => _dateTimeStamp;
            set
            {
                _dateTimeStamp = value;
                _dateTime = DateTime.ParseExact(_dateTimeStamp, Globals.DateTimeStringFormat, CultureInfo.InvariantCulture);
            }
        }

        private DateTime _lastRepeated;
        public DateTime LastRepeated
        {
            get => _lastRepeated;
            set
            {
                _lastRepeated = value;
                _lastRepeatedTicks = _lastRepeated.Ticks;
            }
        }

        private long _lastRepeatedTicks;
        public long LastRepeatedTicks
        {
            get => _lastRepeatedTicks;
            set
            {
                _lastRepeatedTicks = value;
                _lastRepeated = new DateTime(_lastRepeatedTicks);
            }
        }
    }
}
