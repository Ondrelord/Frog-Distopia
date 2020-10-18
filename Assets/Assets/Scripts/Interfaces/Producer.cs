using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Producer 
{
    // products that can be created
    public Product[] products;

    // currently producing
    Product producing = null;
    // products to be produced
    Queue<Product> productQueue = new Queue<Product>();
    [SerializeField] int productMaxQueueLenght = 5;

    // progress of current production
    float producingProgress;
    [SerializeField] UnityEngine.UI.Image producingProgressBar = null;

    public event EventHandler CreateProduct;
    bool productReady = false;
    Product productDone;

    public void Setup()
    {
        GameObject.FindObjectOfType<TimeManager>().Tick += Producer_Tick;
    }

    private void Producer_Tick(object sender, EventArgs e)
    {
        if (Working() && producingProgress <= producing.GetTimeToProduce())
            producingProgress += Time.deltaTime;
    }

    // Command to Start producing (or enqueue if already producing).
    public void Produce(int index)
    {
        Product product = GetProduct(index);

        if (product == null)
            return;

        if (producing == null)
            producing = product;
        else if (productQueue.Count < productMaxQueueLenght)
            productQueue.Enqueue(product);

    }

    // Update loop function. Continues producing until no products are in queue.
    public void Producing()
    {
        // contiue in production
        if (producing != null)
        {
            if (producingProgress < producing.GetTimeToProduce())
            {
                producingProgressBar.fillAmount = producingProgress / producing.GetTimeToProduce();
            }
            else
            {
                // create product
                productReady = true;
                productDone = producing;
                CreateProduct?.Invoke(this, EventArgs.Empty);
                // reset progress
                producingProgress = 0f;
                producingProgressBar.fillAmount = 0f;
                // next in queue
                if (productQueue.Count != 0)
                    producing = productQueue.Dequeue();
                else
                    producing = null;
            }
        }
    }

    // True if producer is stil producing.
    public bool Working() => producing != null;

    // Returns product ready to instantiate. Null if not ready.
    public Product GetDoneProduct()
    {
        if (productReady)
        {
            productReady = false;
            return productDone;
        }

        return null;
    }

    // Returns product from available options
    public Product GetProduct(int index)
    {
        if (index < products.Length)
            return products[index];

        return null;
    }

}
