using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentEnrollment.Models
{
    public class Location : Model
    { 
        public Location(int id, int capacity, int roomNumber, int buildingNumber)
        {
            this.ID = id;
            this.Capacity = capacity;
            this.RoomNumber = roomNumber;
            this.BuildingNumber = buildingNumber;
        }

        public int ID { get; }
        public int Capacity { get; }
        public int RoomNumber { get; }
        public int BuildingNumber { get; }

        public override string ToString()
        {
            return String.Format("Location #{0}: \nCapacity: {1} \nBuilding {2} #{3}", this.ID, this.Capacity, this.RoomNumber, this.BuildingNumber);
        }
    }
}
