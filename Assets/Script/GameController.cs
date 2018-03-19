﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    //people location
    private Vector2 PeopleCreatePoint;
    public float MaxY = 5f;        // screen`s max y  To define people`s create point 
    public float MaxX = 12f;       //screen`s max x   
    public float TopCreateVar = 4f; // define top create-zone
    public GameObject MiddleZone;
    //people location

    //about people
    public GameObject Man;           //一般人型 

    private GameObject People;          //the name of every human to manage them
    private int CreateDirection = 0;    //0=up 1=left 2=right
    private int PeopleCount = 0;        //give every people a number to make a list

    private float PeopleCreateSpeed; //the speed of people-creating
    private int PeopleKilling;       //the amount of killed people

    private List<string> CustomerList = new List<string>();
    private float PofCustomers = 0.8f;  // the probability of be a customer  range from 0 to 1 
    //about people


    //about fish
    public GameObject Maguro;        //list code 1
    public GameObject Tako;          //list code 2
    public GameObject Kani;          //list code 3
    public GameObject Fugu;          //list code 4
    public float CreateSpeed = 2.0f;
    public Vector3 FishPosition = new Vector3(0f, -3f, 6f);
    //about fish

    //about Sushi
    private int SushinameCount = 0;
    public GameObject MaguroSushi;     
    //about Sushi


    //about point and eating sushi
    public Text MoneyText;
    public Text PeoplekillText; 
    private int Money = 0;            //dont forget to intialize it! 
    private bool CustomerFlag = true;   //used to invoke the time of people eat sushi
        //price
    private int NormalPrice = 100;
    private int GoldPrice = 200;
    private int PisionPrice = 100;
        //price
    private List<int> SushiList = new List<int>(); //know the types of sushi
    /*    1 ====================Maguro-normalprice
          2 ====================Fugu--goldprice
          3 ====================poison--normalprice                     */
    private GameObject Sushi;
    private GameObject SushiDelete;
    private int SushiDeleteCount = 0;
    private float CustomerWaitTime = 1.0f;
    private GameObject peopleEatingSushi;
    private float CheckTime = 3.0f;
    //about point and eating sushi



    //about ninja
    //about ninja



 


//------------------------------fuction--------------------------------------------------------------------


	// Use this for initialization
	void Start () {
        create_people();
        create_fish();
        CheckLoop();
	}
	
	// Update is called once per frame
	void Update ()
    {
        customers_list_check();
    }
    /// <summary>
    ///  ever CHeckTime check eat sushi one time
    /// </summary>
    void CheckLoop()
    {
        if (CustomerFlag==true)
        {
            eat_sushi();
        }
        Invoke("CheckLoop",CheckTime);
    }

    /// <summary>
    /// change state with different popular value
    /// </summary>
    void popular_check()
    {

    }

    /// <summary>
    /// create fish (define amount/type/point/etc.)
    /// </summary>
    void create_fish()
    {
        //fish amount choose (uncomplete)(related to popular)
        //fish amount choose (uncomplete)(related to popular)

        //fish type choose(uncomplete)
        float ProbabilityFish = 0;
        ProbabilityFish = Random.Range(0.0f, 1.0f);
        if(ProbabilityFish>=0f && ProbabilityFish < 0.6f)
        {
            Instantiate(Maguro, FishPosition, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
            Invoke("create_fish", CreateSpeed);
        }else if (ProbabilityFish < 0.8f)
        {
            Instantiate(Maguro, FishPosition, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
            Invoke("create_fish", CreateSpeed);
        }
        else
        {
            Instantiate(Maguro, FishPosition, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
            Invoke("create_fish", CreateSpeed);
        }                        
        //fisu type choose (uncompleter)
    }

    /// <summary>
    ///  create people(amount/type/point/etc.)
    /// </summary>
    void create_people()
    {
        //addpeople choose(uncomplete)
        //addpeople choose(uncomplete)


        int i = Random.Range(0, 3);
        if (i == 0)      //create at up
        {
            float x = Random.Range(MiddleZone.transform.position.x - TopCreateVar, 
                MiddleZone.transform.position.x + TopCreateVar);
            PeopleCreatePoint = new Vector2(x, MaxY);
            People = Instantiate(Man, PeopleCreatePoint, Quaternion.identity);
            float j = Random.Range(0.0f, 1.0f); //compare to p of being a customer
            bool BeCustomer = false;
            if (j < PofCustomers)               //be a customer
            {
                BeCustomer = true;
                customers_listadd("People" + PeopleCount);   //add this name to customers list
            } 
                People.SendMessage("enter_from_up",BeCustomer);
            People.name = "People" + PeopleCount;
            PeopleCount++;            
        }
        else if (i == 1)   //create at left
        {
            float y = Random.Range(MiddleZone.transform.position.y-MiddleZone.GetComponent<BoxCollider2D>().size.y/2,
                MiddleZone.transform.position.y+ MiddleZone.GetComponent<BoxCollider2D>().size.y / 2);
            PeopleCreatePoint = new Vector2(-MaxX, y);
            People = Instantiate(Man, PeopleCreatePoint, Quaternion.identity);
            float j = Random.Range(0.0f, 1.0f); //compare to p of being a customer
            bool BeCustomer = false;
            if (j < PofCustomers)               //be a customer
            {
                BeCustomer = true;
                customers_listadd("People" + PeopleCount);    //add this name to customers list
            }
            People.SendMessage("enter_from_left",BeCustomer);
            People.name = "People" + PeopleCount;
            PeopleCount++;
        }
        else if(i == 2)  //create at right
        {
            float y = Random.Range(MiddleZone.transform.position.y - MiddleZone.GetComponent<BoxCollider2D>().size.y / 2,
                MiddleZone.transform.position.y + MiddleZone.GetComponent<BoxCollider2D>().size.y / 2);
            PeopleCreatePoint = new Vector2(MaxX, y);
            People = Instantiate(Man, PeopleCreatePoint, Quaternion.identity);
            float j = Random.Range(0.0f, 1.0f); //compare to p of being a customer
            bool BeCustomer = false;
            if (j < PofCustomers)               //be a customer
            {
                BeCustomer = true;
                customers_listadd("People" + PeopleCount);  //add this name to customers list
            }
                People.SendMessage("enter_from_right",BeCustomer);
            People.name = "People" + PeopleCount;
            PeopleCount++;
        }

        PeopleCreateSpeed = Random.Range(0.3f, 2f);       //speed of create speed 
        Invoke("create_people", PeopleCreateSpeed);
    }

    /// <summary>
    ///  to manage the list of waiting customer
    /// </summary>
    /// <param name="ObjectName"></param>
    void customers_listadd(string ObjectName)
    {
        CustomerList.Add(ObjectName);
        People.SendMessage("customers_check2", (CustomerList.Count - 1));

    }
    /// <summary>
    /// to check the amount of customers now  
    /// change with popular
    /// </summary>
    void customers_list_check()
    {
        if (CustomerList.Count > 5)
        {
            PofCustomers = 0.0f;
        }
        else
        {
            PofCustomers = 0.8f;
        }
    }


    void kill_people(string name)
    {
        PeopleKilling = PeopleKilling + 1;

        CustomerList.Remove(name);
        customers_manage(1);    //only change number
       // PeoplekillText.text = ("殺人数" + PeopleKilling.ToString());
    }
    /// <summary>
    /// check after (eating sushi)(int i=0)/(kill people)(int i=1)
    /// </summary>
    void customers_manage(int i)
    {
        if (i == 1)      //people will leave if you kill people
        {
            int k = CustomerList.Count-1;
            for (int j = k; j >1; j--)
            {
                float l=Random.Range(0.0f, 1.0f);
                if (l < 0.05f)
                {
                    GameObject CustomersTemp = GameObject.Find(CustomerList[j]);
                    CustomersTemp.SendMessage("customers_check1");
                    CustomerList.RemoveAt(j);
                }
            }
            for (int j = 0; j < CustomerList.Count; j++)
            {
                GameObject CustomersTemp = GameObject.Find(CustomerList[j]);
                CustomersTemp.SendMessage("customers_check2", j);
            }
        }
        if (i == 0)
        {
            for (int j = 0; j < CustomerList.Count; j++)
            {
                GameObject CustomersTemp = GameObject.Find(CustomerList[j]);
                CustomersTemp.SendMessage("customers_check2", j);
            }
        }
    }

    /// <summary>
    /// add Maguro sushi to list (outside used)
    /// </summary>
    /// <param name="position"></param>
    void create_magurosushi(Vector2 position)
    {
        Vector3 SushiPosition = new Vector3(position.x,position.y, 8);
        Sushi = Instantiate(MaguroSushi,SushiPosition,Quaternion.identity);
        Sushi.name = "Sushi" + SushinameCount;
        SushinameCount++;
        SushiList.Add(1);         //add maguro(code 1)to sushi list
    }

    /// <summary>
    /// people eat sushi and get point 
    /// </summary>
    void eat_sushi()
    {
        if(SushiList.Count>0 && CustomerList.Count > 0)     //customer exist and sushi exist
        {
            int SushiType = SushiList[0];
            //different point for different sushi
            switch (SushiList[0])
            {
                case 1:                                     //maguro
                    Money = Money + NormalPrice;
                    break;
                case 2:
                    Money = Money + GoldPrice;
                    break;
                case 3:
                    Money = Money + NormalPrice;
                    break;
            }
            //MoneyText.text = Money.ToString();

            SushiList.RemoveAt(0);

            SushiDelete = GameObject.Find("Sushi" + SushiDeleteCount);  //find next sushi to delete
            Destroy(SushiDelete.gameObject);
            SushiDeleteCount++;

            //delete customer
            peopleEatingSushi = GameObject.Find(CustomerList[0]);
            peopleEatingSushi.SendMessage("after_eatsushi",SushiType); //send type (poision--->die)
            CustomerFlag = false;
            Invoke("customer_flag_set", CustomerWaitTime);
        }
    }
    void customer_flag_set()
    {
        CustomerFlag = true;
        CustomerList.RemoveAt(0);
        customers_manage(0);
    }
}
