using System;

namespace ChapeauModel
{
    public class Table
    {
        public enum TableStatus
        {
            Free, Occupied, Reserved, Ordered, Undefined
        }

        public int TableId { get; set; }
        public TableStatus Status { get; set; }
        public int Capacity { get; set; }


        public static TableStatus StringToStatus(string status)
        {
            return status switch
            {
                "Free" => TableStatus.Free,
                "Occupied" => TableStatus.Occupied,
                "Reserved" => TableStatus.Reserved,
                "Ordered" => TableStatus.Ordered,
                _ => TableStatus.Undefined,
            };
        }

        public static string StatusToString(TableStatus status)
        {
            return status switch
            {
                TableStatus.Free => "Free",
                TableStatus.Occupied => "Occupied",
                TableStatus.Reserved => "Reserved",
                TableStatus.Ordered => "Ordered",
                _ => "Undefined",
            };
        }
    }
}
