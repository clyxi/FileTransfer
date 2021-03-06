﻿using NHibernate.Mapping.Attributes;
using System;

namespace FileTransfer.DbHelper.Entitys
{
    [Class(Table = "MonitorLog")]
    public class MonitorLogEntity
    {
        [Id(Name = "ID", TypeType = typeof(long), Column = "ID")]
        [Key(1)]
        [Generator(2, Class = "assigned")]
        public virtual long ID { get; set; }
        [Property(Column = "MonitorDate", TypeType = typeof(DateTime))]
        public virtual DateTime MonitorDate { get; set; }
        [Property(Column = "ChangedFile", TypeType = typeof(string))]
        public virtual string ChangedFile { get; set; }

        public MonitorLogEntity()
        { }

        public MonitorLogEntity(DateTime monitorDate, string changedFile)
        {
            MonitorDate = monitorDate;
            ChangedFile = changedFile;
        }
    }
}
