using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentEnrollment.Models
{
    public class Location : Model
    { 
        public Location(int id, int capacity, int roomNumber, int buildingID)
        {
            this.ID = id;
            this.Capacity = capacity;
            this.RoomNumber = roomNumber;
            this.BuildingID = buildingID;
        }

        public int ID { get; }
        public int Capacity { get; }
        public int RoomNumber { get; }
        public int BuildingID { get; }

        public override string ToString()
        {
            //TODO: this toString doesn't look quite right.
            return String.Format("Location #{0}: \nCapacity: {1} \nBuilding {2} #{3}", this.ID, this.Capacity, this.RoomNumber, this.BuildingID);
        }
    }
}
