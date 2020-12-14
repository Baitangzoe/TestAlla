using System;

namespace ConsoleArr
{
    //选择排序
    class SelectionSorter
    {
        public static void Sort(int[] arr)
        {
            int temp;
            int minVal;
            int index;
            for (int i = 0; i < arr.Length - 1; i++)
            {
                minVal = arr[i];//假设i下标就是最小的数
                index = i;//记录我认为最小的数的下标
                          //找出这一趟最小的数值并记录下他的下标
                for (int j = i + 1; j < arr.Length; j++)
                {
                    //说明我们认为的最小值，不是最小
                    //这里大于号是升序（大于是找出最小值） 小于号是降序（小于是找出最大值）
                    if (minVal > arr[j])
                    {
                        minVal = arr[j];//更新这趟最小（或最大）的值（上面要拿这个数来跟后面的数继续做比较）
                        index = j;//记下它的坐标
                    }
                }
                temp = arr[i];//把第一个原先认为是最小值的数，临时保存起来
                arr[i] = arr[index];//把最终找到的最小值赋给这一趟的比较的第一个位置
                arr[index] = temp;//把原先保存好临时数值放回此数组的空地方，保证数组的完整性
            }
        }
    }
    //冒泡排序
    class EbullitionsSorter
    {
        public static void Sort(int[] arr)
        {
            int temp = 0;
            //外循环表示循环的趟数
            for (int i = 0; i < arr.Length - 1; i++)
            {
                //内循环表示冒泡的处理
                for (int j = 0; j < arr.Length - i - 1; j++)
                {
                    //通过比较大小，实现元素交换
                    if (arr[j] > arr[j + 1])
                    {
                        temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                    }
                }
            }
        }
    }
    //快速排序
    class QuickSorter
    {
        public static void SordPX(int[] arr, int left, int right)
        {
            //将数组第一个数给临时变量temp，并找到temp的索引位置
            int temp = arr[left];//自左侧挖一个坑(将15拿出来同接下来的数值作比较)
            int i = left;
            int j = right;
            //疯狂挖坑
            while (i < j)
            {
                //后标记前移
                while (arr[j] >= temp && i < j)
                {
                    j--;
                }
                if (arr[j] <= temp)
                {
                    temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;//填上最后的坑（找到中心15）
                }
                //前标记后移
                while (arr[i] <= temp && i < j)
                {
                    i++;
                }
                if (arr[j] <= temp)
                {
                    temp = arr[j];
                    arr[j] = arr[i];
                    arr[i] = temp;//填上最后的坑（找到中心15）
                }
            }
            //递归（直接或间接的调用自己的算法）
            if (i > left)
            {
                SordPX(arr, left, i - 1);
            }
            if (j < right)
            {
                SordPX(arr, j + 1, right);
            }
        }
    }
    //插入排序
    class InsertionSorter
    {
        public static void Sort(int[] arr)
        {
            int temp;//预备插入有序数组中的数
            int j;//下标
                  //插入排序是把无序的数一个个插入到有序的数
                  //先默认下标为0的这个数已经有序（把无序数组中的第一个数（从左向右数）拿出来，放到有序数组中的最右侧）
                  //自无序数组中的第二个数开始比较
            for (int i = 1; i < arr.Length; i++)
            {
                temp = arr[i];
                j = i - 1;
                //当下标>=0并且arr[1]<arr[0]
                //也就是当第二个数<有序数组中的数
                //用<确保相同元素的相对次序不变，而不用<=
                while (j >= 0 && temp < arr[j])
                {
                    //将大的元素向后移，而不是每比较完就交换
                    arr[j + 1] = arr[j];
                    //Debug.Log(arr[j + 1]);//9,6,4
                    j--;//从右往左比较，所以--，而不是++
                }
                //插入  找到合适的位置
                arr[j + 1] = temp;
                //Debug.Log(arr[insertIndex + 1]);//9,4,2,3
            }
        }
    }
    //希尔排序
    class ShellSorter
    {
        public static void Sort(int[] arr)
        {
            int gap = arr.Length / 2;
            while (gap >= 1)
            {
                //把距离为gap的元素编为一个组，扫描所有组
                for (int i = gap; i < arr.Length; i++)
                {
                    int temp = arr[i];//存储和其比较的上一个元素
                    int j = 0;
                    //对距离为gap的元素组进行排序
                    for (j = i - gap; j >= 0 && temp < arr[j]; j = j - gap)
                    {

                        arr[j + gap] = arr[j];
                    }
                    arr[j + gap] = temp;
                }
                gap = gap / 2;//减小增量
            }
        }
    }
    class Demo
    {
        static void Main(string[] args)
        {
            //快速排序
            Console.WriteLine("快速排序：");
            int[] arr1 = new int[] { 15, 7, 32, 72, 12, 65, 28, 125, 2, 57 };
            QuickSorter.SordPX(arr1, 0, arr1.Length - 1);
            for (int i = 0; i < arr1.Length; i++)
            {
                Console.WriteLine(arr1[i] + "\t");
            }
            //插入排序
            Console.WriteLine("插入排序：");
            int[] arr2 = new int[5] { 6, 9, 4, 2, 3 };
            InsertionSorter.Sort(arr2);
            for (int a = 0; a < arr2.Length; a++)
            {
                int b = a + 1;
                Console.WriteLine("第" + b + "个数：" + arr2[a]);
            }
            //希尔排序
            Console.WriteLine("希尔排序：");
            int[] arr3 = new int[] { 9, 1, 2, 5, 7, 4, 8, 6, 3, 5 };
            ShellSorter.Sort(arr3);
            foreach (int n in arr3)
            {
                Console.WriteLine(n);
            }
            //冒泡排序
            Console.WriteLine("冒泡排序：");
            int[] arr4 = new int[] { 2, 34, 456, 3320, 2675, 3268 };
            EbullitionsSorter.Sort(arr4);
            //利用for循环将元素逐个显示出来
            for (int a = 0; a < arr4.Length; a++)
            {
                Console.WriteLine(arr4[a] + "\t");
            }
            //选择排序
            Console.WriteLine("选择排序：");
            int[] arr5 = new int[8] { 15, 0, 10, 50, 55, 35, 12, 20 };//待排序数组
            SelectionSorter.Sort(arr5);
            foreach (int item in arr5)
            {
                Console.WriteLine(item);
            }
            Console.ReadKey();
        }
    }
}
