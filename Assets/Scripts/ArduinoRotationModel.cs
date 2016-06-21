using System;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public class ArduinoRotationModel
    {
        public ArduinoRotationModel()
        {
            euler = new List<euler>();
            quaternion = new List<quaternion>();
        }

        public List<euler> euler { get; set; }
        public List<quaternion> quaternion { get; set; }
    }

    public class euler
    {
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
    }

    public class quaternion
    {
        public float w { get; set; }
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
    }
}
