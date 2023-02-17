using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using EllipticCurve;

namespace PCoin
{
    class Program
    {
        static void Main(string[] args)
        {
            PrivateKey key1 = new PrivateKey();
            PublicKey wallet1 = key1.publicKey();

            PrivateKey key2 = new PrivateKey();
            PublicKey wallet2 = key2.publicKey();
            
            Blockchain pcoin = new Blockchain(3, 100); // First parameter is the difficulty. 2 means that hashes will have to be generated until there are 2 zeros at the beginning of the hash

            Console.WriteLine("Start the Miner.");
            pcoin.MinePendingTransactions(wallet1);
            Console.WriteLine("\nBalance of wallet1 is $" + pcoin.GetBalanceOfWallet(wallet1).ToString());

            Transaction tx1 = new Transaction(wallet1, wallet2, 10);
            tx1.SignTransaction(key1);
            pcoin.addPendingTransaction(tx1);
            Console.WriteLine("Start the Miner.");
            pcoin.MinePendingTransactions(wallet2);
            Console.WriteLine("\nBalance of wallet1 is $" + pcoin.GetBalanceOfWallet(wallet1).ToString());
            Console.WriteLine("\nBalance of wallet2 is $" + pcoin.GetBalanceOfWallet(wallet2).ToString());

            string blockJSON = JsonConvert.SerializeObject(pcoin, Formatting.Indented);
            Console.WriteLine(blockJSON);

            // pcoin.GetLatestBlock().PreviousHash = "12345";

            // Run validation function
            if (pcoin.IsChainValid())
            {
                Console.WriteLine("Blockchain is Valid!");
            }
            else
            {
                Console.WriteLine("Blockchain is NOT valid.");
            }
        }
    }
}
