using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToyTwoToolbox {
    public class Animation {
        public int id; //4byte
        public int id2; //4byte
        public int FrameCount; //the number of frames for this animation
        public int UNK1;
        public int UNK2;
        public int NodeCount;
        public int UNK3;
        public int HPTR; //the start of the nodes, 4 bytes
        public List<Animation.Node> Nodes;
        public int UNK4;
        public int UNK5;
        public int UNK6;
        public int UNK7;
        public byte[] extradata;
        
        public Animation() {
            Nodes = new List<Node>();
        }


        public class Node {
            public int id;
            public int offset;
            public List<AnimationFrame> frames; //the frames available for this character's node

            public Node() {
                id = 0;
                offset = 0;
                frames = new List<AnimationFrame>();
            }
        }

    }
}
