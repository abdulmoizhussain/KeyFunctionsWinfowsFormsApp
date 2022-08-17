using KeyFunctions.Common.Enums;
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
        private const string s_format = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff";

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
                _dateTimeStamp = _dateTime.ToString(s_format);
            }
        }

        public string _dateTimeStamp;
        public string DateTimeStamp
        {
            get => _dateTimeStamp;
            set
            {
                _dateTimeStamp = value;
                _dateTime = DateTime.ParseExact(_dateTimeStamp, s_format, CultureInfo.InvariantCulture);
            }
        }
    }
}
