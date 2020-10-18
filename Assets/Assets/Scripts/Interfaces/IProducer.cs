using System;
using UnityEngine;

public interface IProducer
{
    Producer GetProducer();
    void Produce();
    void StartProducing(int index);
    void CreateProduct(object sender, EventArgs e);
}
