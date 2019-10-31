/***
*
*     Title: ""
*           主题：单例模板类
*     Description:
*           功能：实现单例模式，作为模板类
*           使用时，只需要访问Instance就行
*           例：
*           public Test instance
*           {
*               get{return Singleton<Test>.Instance;}
*           }
*
*      Date:2017
*      CLR Version:0.1
*      create by HHUC on 7/19/2017
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleUIFDemo
{
    // legal
	//public class Singleton<T> : MonoBehaviour where T:new()

	public class Singleton<T> where T : MonoBehaviour, new()
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (Singleton<T>._instance == null)
                {
                    // creat a gameobject with T instance
                    Singleton<T>._instance = new GameObject(typeof(T).ToString()).AddComponent<T>();
                }
                return Singleton<T>._instance;
            }
        }
	}

}
