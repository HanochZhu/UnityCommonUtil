using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Utils
{

    public static class ModelUtils
    {

        public static void SaveVerticesToObjFile(List<Vector3> vertices, string path)
        {
            if (vertices.Count != 14)
            {
                Debug.LogFormat("{0},{1}", vertices.Count, 15);
                return;
            }
            string obj_content = "";
            foreach (Vector3 item in vertices)
            {
                obj_content += string.Format("v {0} {1} {2}\n", item.x, item.y, item.z);
            }
            /*construct face structure*/
            obj_content += "f 1 2 4\n" +
                            "f 1 4 3\n" +
                            "f 3 4 6\n" +
                            "f 6 4 10\n" +
                            "f 6 10 8\n" +
                            "f 8 10 11\n" +
                            "f 11 10 13\n" +
                            "f 13 10 14\n" +
                            "f 14 10 12\n" +
                            "f 12 10 9\n" +
                            "f 9 10 7\n" +
                            "f 7 10 4\n" +
                            "f 7 4 5\n" +
                            "f 5 4 2\n" +
                            "f 2 4 1\n";
            /*write content to file*/
            Utils.FileUtils.WriteStringToFile(path, obj_content);
        }

        public static void SaveVerticesToObjFile(List<Vector4> vertices, string path)
        {
            if (vertices.Count != 14)
            {
                Debug.LogFormat("{0},{1}", vertices.Count, 15);
                return;
            }
            string obj_content = "";
            foreach (Vector3 item in vertices)
            {
                obj_content += string.Format("v {0} {1} {2}\n", item.x, item.y, item.z);
            }
            /*construct face structure*/
            obj_content += "f 1 2 4\n" +
                            "f 1 4 3\n" +
                            "f 3 4 6\n" +
                            "f 6 4 10\n" +
                            "f 6 10 8\n" +
                            "f 8 10 11\n" +
                            "f 11 10 13\n" +
                            "f 13 10 14\n" +
                            "f 14 10 12\n" +
                            "f 12 10 9\n" +
                            "f 9 10 7\n" +
                            "f 7 10 4\n" +
                            "f 7 4 5\n" +
                            "f 5 4 2\n" +
                            "f 2 4 1\n";
            /*write content to file*/
            Utils.FileUtils.WriteStringToFile(path, obj_content);
        }

        // get distance by euclidean metric
        public static float EuclideanSimilarity(List<Vector4> sourcesPos,List<Vector4> targetsPos,int centerPointIndex = 3)
        {
            Vector4 source_pos_center = sourcesPos[centerPointIndex];
            Vector4 target_pos_center = targetsPos[centerPointIndex];

            int[] ignoreIndex = { 3, 9 };

            int length = sourcesPos.Count;

            for (int i = 0; i < length; i++)
            {
                if (i == 3 || i == 9)/*hard code for the moment */
                {
                    continue;
                }
                // 
            }

            

            return 0.0f;
        }

        // cal similarity by cos algorithm
        public static float CosSimilarity(List<Vector4> sourcesPos, List<Vector4> targetsPos, int centerPointIndex = 3)
        {
            VectorMultiNumF a = new VectorMultiNumF();
            VectorMultiNumF b = new VectorMultiNumF();
            Vector4 source_pos_center = sourcesPos[centerPointIndex];
            Vector4 target_pos_center = targetsPos[centerPointIndex];

            int length = sourcesPos.Count;

            for (int i = 0; i < length; i++)
            {
                // ignore some point
                //if (i == 3 || i == 9)
                //{
                //    continue;
                //}
                float sourcesdistance = Vector4.Distance(sourcesPos[i], source_pos_center);
                float targetDistance = Vector4.Distance(targetsPos[i], target_pos_center);
                a.AddAItem(sourcesdistance);
                b.AddAItem(targetDistance);
            }
            return VectorMultiNumF.CosDistance(a, b);
        }

        public class VectorMultiNumF
        {
            private List<float> vector;

            public int Length
            {
                get
                {
                    return vector.Count;
                }
            }

            public float this[int index]
            {
                get
                { /* return the specified index here */
                    return vector[index];
                }
                set
                { /* set the specified index to value here */
                    vector[index] = value;
                }
            }

            public VectorMultiNumF()
            {
                vector = new List<float>();
            }

            public void AddAItem(float Item)
            {
                vector.Add(Item);
            }

            public static float CosDistance(VectorMultiNumF a,VectorMultiNumF b)
            {
                // additional function call will increase process time 
                if (!checklegality(a,b))
                {
                    return 0.0f;
                }
                // cos = a.b/(||a||.||b||)
                return Dot(a, b) / (magnitude(a) * magnitude(b));
            }

            // second Normal Form: 
            // in Geometrically : Second Normal form is equal to distance from origin to vector
            public static float magnitude(VectorMultiNumF a)
            {
                // formula:
                //       __________________
                //   2 /  2   2         2
                //  /\/  a + b + ... + n
                float result = 0.0f;
                foreach (var item in a.vector)
                {
                    result += item * item;
                }
                return Mathf.Sqrt(result);
            }

            public static float Dot(VectorMultiNumF a, VectorMultiNumF b)
            {
                if (!checklegality(a, b))
                {
                    return 0.0f;
                }
                float result = 0.0f;
                int length = a.Length;
                for (int i = 0; i < length; i++)
                {
                    result += a[i] * b[i];
                }
                return result;
            }

            //check: is two vector participate in calculation legal
            public static bool checklegality(VectorMultiNumF a, VectorMultiNumF b)
            {
                if (a.Length != b.Length)
                {
                    Debug.LogFormat("calculate error with different length a :{},b{}", a.Length, b.Length);
                    return false;
                }
                return true;
            }
        }

    }
}
