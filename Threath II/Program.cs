using System;
using System.Threading;

namespace Threath_II
{
    class Program
    {
        static void Main(string[] args)
        {
            CuentaBancaria cuentaFamilia = new CuentaBancaria(2000);

            Thread[] hilosPersonas = new Thread[15];

            for (int i = 0; i < 15; i++)
            {
                Thread t = new Thread(cuentaFamilia.VamosRetirarEfectivo);
                //se le asigna el nombre con el valor de hi
              t.Name = "T" + i.ToString();
                //cada posicion del array armacena un threath
             
                hilosPersonas[i] = t;
            }
            //lee los hilos
            for (int i = 0; i < 15; i++) hilosPersonas[i].Start();
           
        }
    }

    class CuentaBancaria
    {
        private object bloqueaSaldoPositivo = new object();
        double Saldo { set; get; }

        public CuentaBancaria(double Saldo)
        {
            this.Saldo = Saldo;

        }

        public double RetirarEfectuvo(double cantidad)
        {
            if((Saldo-cantidad)<0)
            {
                //CurrentThreath dice el nombre
                Console.WriteLine($"Lo siento queda ${Saldo} DOP en la cuenta Hilo: {Thread.CurrentThread.Name}");

                return Saldo;
            }
            //Esta parte se ejecuta un hilo de forma simultanea para eso sirve el lock
            //Cuando accede un threath este trozo de codigo queda bloqueado
            lock (bloqueaSaldoPositivo)
            {
                if (Saldo >= cantidad)
                {
                    Console.WriteLine("Retirado {0} y queda {1} en la cuenta {2}", cantidad, (Saldo - cantidad), Thread.CurrentThread.Name);
                    Saldo -= cantidad;

                    return Saldo;
                }

                return Saldo;
            }
        }
        //un metodo que llame a otro metodo para retirar efectivo en donde cada uno retira efectivo
        public void VamosRetirarEfectivo()

        {
            Console.WriteLine("Esta sacando dinero en hilo: {0}", Thread.CurrentThread.Name);
           for(int i=0; i<4;i++) RetirarEfectuvo(500);
        }
    }
}
