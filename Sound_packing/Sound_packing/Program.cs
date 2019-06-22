using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace draftttttt
{
    class Program
    {

        static void Main()
        {
            while (true)
            {
                Console.WriteLine("Enter [1] to use Worst Fit");
                Console.WriteLine("Enter [2] to use Worst Fit Decreasing");
                Console.WriteLine("Enter your choice: ");
                int choice = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter folder size: ");
                int folder_size = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter text file name: ");
                string textfile_name = Console.ReadLine();

                FileStream fs = new FileStream(textfile_name + ".txt", FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                int x = int.Parse(sr.ReadLine());           // read number of sound records
                int[] freq = new int[100000];               // folders
                int[] nums = new int[100000];               // durations
                string[] arr = new string[2];               // two strings represent files names and duration 
                string[] times = new string[3];             // string array holds hours ,minutes,seconds separated 
                string folderf = "";                        // path to create folder to hold sound records
                string meta_data = "";                      // path to cteate metadata text file  
                int tempp = 0;
                int num_text = 0;                           //to count how many text file 
                List<Tuple<string, string>> mylist = new List<Tuple<string, string>>();//like vetor off pair to hold name and duration of each record
                Tuple<string, string> tup;
                List<List<Tuple<string, string>>> folders = new List<List<Tuple<string, string>>>();
                List<Tuple<string, string>> files = new List<Tuple<string, string>>();
                Tuple<string, string> tupl;
                var watch = new System.Diagnostics.Stopwatch();

                for (int i = 0; i < x; i++)
                {
                    string y = sr.ReadLine();
                    arr = y.Split(' ');             //arr[0] string hold sound records names, arr[1] string hold this shape h:m:s
                    times = arr[1].Split(':');
                    nums[i] = (int.Parse(times[0])) * 3600 + (int.Parse(times[1])) * 60 + (int.Parse(times[2]));//transform h:m:s to seconds                      
                    tup = new Tuple<string, string>(arr[0], arr[1]);  //like vector of pair to hold name and duration of each record
                    mylist.Add(tup);                                  //add tuple to the list

                }
                sr.Close();  //I close the file to be able to open it in the same time again
                watch.Start(); //start measuring time 
                if (choice == 1)
                {
                    string folder_1 = @"output1\[1]WorstFit";
                    folderf = @"output1\[1]WorstFit\f";   //path to create folder to hold sound records
                    meta_data = @"output1\[1]WorstFit\f"; //path to cteate metadata text file  
                    System.IO.Directory.CreateDirectory(folder_1);                             //create folder to worstfit
                }
                if (choice == 2)
                {
                    int l = 0;    //left of the nums array
                    int r = x - 1;    //right of the nums array
                    MergeSort(nums, l, r, mylist);
                    string folder_2 = @"output1\[2]WorstFit Decreasing";
                    System.IO.Directory.CreateDirectory(folder_2);                                        //create folder for worstfit decreaing
                    folderf = @"output1\[2]WorstFit Decreasing\f";   //path to create folder to hold sound records
                    meta_data = @"output1\[2]WorstFit Decreasing\f"; //path to cteate metadata text file  
                }
                //Linear Search
                for (int i = 0; i < x; i++)
                {
                    freq[i] = folder_size;      //initialize folder by folder size
                    if (i == 0)                 //if this is the first record create new folder and push the record
                    {
                        freq[0] -= nums[0];                                                 //folder capacity minus the sound duration 
                        tupl = new Tuple<string, string>(mylist[0].Item1, mylist[0].Item2);
                        files.Insert(0, tupl);
                        folders.Add(files);

                        //System.IO.Directory.CreateDirectory(folderf + (i + 1));             //create folder to hold sound records
                        //using (StreamWriter sw = File.CreateText(meta_data + (i + 1) + "_METADATA.txt"))
                        //{
                        //    num_text++;
                        //    sw.WriteLine("f" + (i + 1));
                        //    sw.WriteLine(mylist[0].Item1 + " " + mylist[0].Item2);          //write sound duration in the metadata text file
                        //}
                        //fun(mylist[0].Item1, folderf + (i + 1));                            //call function to copy the files
                    }
                    //if this is not the first record search for the biggest space in the folders that already have records 
                    else
                    {
                        int w = 0;
                        int index = 0;
                        int biggest_index = 0;
                        tempp = 0;
                        //searching for the biggest space in the folders
                        while (freq[w] != folder_size)
                        {
                            biggest_index = w;
                            if (freq[w] > tempp)
                            {
                                index = w;
                                tempp = freq[w];
                            }
                            w++;
                        }
                        if (tempp >= nums[i] && (freq[index] - nums[i]) >= 0)
                        {
                            tupl = new Tuple<string, string>(mylist[0].Item1, mylist[0].Item2);
                            files.Insert(index+1, tupl);
                            folders.Add( files);

                            //System.IO.Directory.CreateDirectory(folderf + (index + 1));  //create folder to hold sound records
                            //if (!File.Exists(meta_data + (index + 1) + "_METADATA.txt"))
                            //{
                            //    // Create a text file to write to.
                            //    using (StreamWriter sw = File.CreateText(meta_data + (index + 1) + "_METADATA.txt"))
                            //    {
                            //        num_text++;
                            //        sw.WriteLine("f" + (index + 1));
                            //        sw.WriteLine(mylist[i].Item1 + " " + mylist[i].Item2);   //write sound duration in the metadata text file
                            //    }
                            //}
                            //else
                            //{
                            //    using (StreamWriter sw = File.AppendText(meta_data + (index + 1) + "_METADATA.txt"))
                            //    {
                            //        sw.WriteLine(mylist[i].Item1 + " " + mylist[i].Item2);   //write sound duration in the metadata text file
                            //    }
                            //}
                            freq[index] -= nums[i];                                                  //folder capacity minus the sound duration 
                            //fun(mylist[i].Item1, folderf + (index + 1));                             //call function to copy the files
                        }
                        if (tempp < nums[i])
                        {
                            tupl = new Tuple<string, string>(mylist[0].Item1, mylist[0].Item2);
                            files.Insert(biggest_index+1, tupl);
                            folders.Add( files);
                            //System.IO.Directory.CreateDirectory(folderf + (biggest_index + 2));  //create folder to hold sound records
                            //if (!File.Exists(meta_data + (biggest_index + 2) + "_METADATA.txt"))
                            //{
                            //    //Create a file to write to.
                            //    using (StreamWriter sw = File.CreateText(meta_data + (biggest_index + 2) + "_METADATA.txt"))
                            //    {
                            //        num_text++;
                            //        sw.WriteLine("f" + (biggest_index + 2));
                            //        sw.WriteLine(mylist[i].Item1 + " " + mylist[i].Item2);   //write sound duration in the metadata text file
                            //    }
                            //}
                            freq[biggest_index + 1] -= nums[i];                                                //folder capacity minus the sound duration 
                            //fun(mylist[i].Item1, folderf + (biggest_index + 2));                               //call function to copy the files
                        }
                    }
                }
                Console.WriteLine("time: " + watch.ElapsedMilliseconds + " ms");

                for (int i = 0; i < num_text; i++)
                {
                    int ii = i + 1;
                    // string timee = ((folder_size - freq[i]) / 3600 + ":" + (folder_size - freq[i]) / 60 + ":" + ((folder_size - freq[i]) - ((folder_size - freq[i]) / 3600) * 3600 - ((folder_size - freq[i]) / 60) * 60));
                    //File.AppendAllText((meta_data + ii + "_METADATA.txt"), timee);
                    for (int w = 0; w < files.Count; w++)
                    {
                        using (StreamWriter sww = File.AppendText(meta_data + ii + "_METADATA.txt"))
                        {
                            string timee = ((folder_size - freq[i]) / 3600 + ":" + (folder_size - freq[i]) / 60 + ":" + ((folder_size - freq[i]) - ((folder_size - freq[i]) / 3600) * 3600 - ((folder_size - freq[i]) / 60) * 60));
                            sww.WriteLine(folders[i][w]);
                            sww.WriteLine(timee); //write total duration for each record 
                         }
                    }
                }
                watch.Stop(); //stop measuring time
                Console.WriteLine("Do you want to continue (y/n) ?");
                string flag = Console.ReadLine();
                if (flag == "y") continue;
                else break;
            }
        }
        static void Merge(int[] a, int low, int high, int mid, List<Tuple<string, string>> list)
        {
            int i = low, j = mid + 1, k = 0;
            int[] temp = new int[high - low + 1];
            List<Tuple<string, string>> templ = new List<Tuple<string, string>>(high - low + 1);
            while (i <= mid && j <= high)
            {
                if (a[i] > a[j])
                {
                    templ.Add(new Tuple<string, string>(" ", " "));
                    templ[k] = Tuple.Create(list[i].Item1, list[i].Item2);
                    temp[k] = a[i];
                    i++;
                    k++;
                }
                //if there is to records have the same size like (15,15) in sample 1 (70,80,20,15,15) 
                //keep them in thier place to make the files as in the sample output
                else if (a[i] == a[j])
                {
                    templ.Add(new Tuple<string, string>(" ", " "));
                    templ[k] = Tuple.Create(list[i].Item1, list[i].Item2);
                    list[i] = Tuple.Create(templ[k].Item1, templ[k].Item2);
                    temp[k] = a[i];
                    i++;
                    k++;
                }
                else if (a[i] < a[j])
                {
                    templ.Add(new Tuple<string, string>(" ", " "));
                    templ[k] = Tuple.Create(list[j].Item1, list[j].Item2);
                    temp[k] = a[j];
                    j++;
                    k++;
                }
            }
            while (i <= mid)
            {
                templ.Add(new Tuple<string, string>(" ", " "));
                templ[k] = Tuple.Create(list[i].Item1, list[i].Item2);
                temp[k] = a[i];
                i++;
                k++;
            }
            while (j <= high)
            {
                templ.Add(new Tuple<string, string>(" ", " "));
                templ[k] = Tuple.Create(list[j].Item1, list[j].Item2);
                temp[k] = a[j];
                j++;
                k++;
            }
            for (i = low; i <= high; i++)
            {
                list[i] = Tuple.Create(templ[i - low].Item1, templ[i - low].Item2);
                a[i] = temp[i - low];
            }
            return;
        }
        static void MergeSort(int[] a, int low, int high, List<Tuple<string, string>> list)
        {
            int mid;
            if (low < high)
            {
                mid = (low + high) / 2;

                MergeSort(a, low, mid, list);
                MergeSort(a, mid + 1, high, list);

                Merge(a, low, high, mid, list);
            }
        }
        static void fun(string file_name, string folderr)
        {
            string fileName = file_name;
            //change that if you want to copy to the source you want copy from ex:(Audios1,Audios2,Audios3,Complete1\Audios,Complete2\Audios,Complete3\Audios)
            string sourcePath = @"Complete1\Audios\" + file_name;
            string slash = "/";
            string targetPath = @folderr + slash + file_name;
            File.Copy(sourcePath, targetPath);
        }
    }
}


