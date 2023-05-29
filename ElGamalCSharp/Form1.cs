﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Numerics;
using System.IO;


namespace ElGamalCSharp
{
    public partial class Form1 : Form
    {
        private readonly int bitLength = 64;
        private static readonly Random random = new Random();
        private BigInteger alpha;
        private BigInteger beta;
        private BigInteger p;
        private BigInteger a;
        public Form1()
        {
            InitializeComponent();
            btnEncrypt.Enabled = false;
            btnDecrypt.Enabled = false;
            btnSendToDecrypt.Enabled = false;
            btnSaveCiphertoFile.Enabled = false;
            btnSavePlaintoFile.Enabled = false;
        }
        

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            Encrypt(boxPlainTextInput.Text);
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            Decrypt(boxCipherTextInput.Text);
        }

        /*private byte[] CutZeroBytes(byte[] bytes)
        {
            int offset = 0;
            while (offset < bytes.Length && bytes[offset] == 0)
            {
                offset++;
            }

            byte[] trimmedBytes = new byte[bytes.Length - offset];
            Buffer.BlockCopy(bytes, offset, trimmedBytes, 0, trimmedBytes.Length);
            return trimmedBytes;
        }*/

        private byte[] Combine(byte[] first, byte[] second)
        {
            byte[] combined = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, combined, 0, first.Length);
            Buffer.BlockCopy(second, 0, combined, first.Length, second.Length);
            return combined;
        }

        private void GenerateKeys()
        {
            BigInteger valp = RandomPrime(bitLength, random);
            BigInteger valalpha = RandomPrimitiveRoot(valp, random);
            BigInteger vala = RandomBigInt(BigInteger.One, valp-BigInteger.One, random);
            BigInteger valbeta = BigInteger.ModPow(valalpha, vala, valp);
            valAlpha.Text = valalpha.ToString();
            valBeta.Text = valbeta.ToString();  //public key
            valP.Text = valp.ToString();
            valA.Text = vala.ToString();  //private key
        }

        private void btnGenerateKey_Click(object sender, EventArgs e)
        {
            GenerateKeys();
            SetKey(true);
        }
        private static BigInteger RandomPrime(int bitLength, Random random)
        {
            BigInteger p;
            do
            {
                byte[] bytes = new byte[bitLength / 8];
                random.NextBytes(bytes);
                p = new BigInteger(bytes);
                p = BigInteger.Abs(p);
                p = BigInteger.Add(p, BigInteger.One);
            } while (!IsPrime(p));
            return p;
        }

        private static bool IsPrime(BigInteger n)
        {
            if (n == BigInteger.One || n == BigInteger.Zero)
            {
                return false;
            }
            if (n == 2 || n == 3)
            {
                return true;
            }
            if (n.IsEven)
            {
                return false;
            }
            //Miller-Rabin primality testing
            int k = 0;
            BigInteger d = n - 1;

            while (d.IsEven)
            {
                d >>= 1;
                k++;
            }

            byte[] bytes = new byte[n.ToByteArray().LongLength];
            BigInteger a;

            for (int i = 0; i < 10; i++)
            {
                do
                {
                    random.NextBytes(bytes);
                    a = new BigInteger(bytes);
                } while (a <= 1 || a >= n - 1);

                BigInteger x = BigInteger.ModPow(a, d, n);

                if (x == 1 || x == n - 1)
                {
                    continue;
                }

                for (int r = 1; r < k; r++)
                {
                    x = BigInteger.ModPow(x, 2, n);

                    if (x == 1)
                    {
                        return false;
                    }

                    if (x == n - 1)
                    {
                        break;
                    }
                }

                if (x != n - 1)
                {
                    return false;
                }
            }
            return true;
        }

        private static BigInteger RandomPrimitiveRoot(BigInteger p, Random random)
        {
            BigInteger phi = p - BigInteger.One;
            HashSet<BigInteger> factors = Factors(phi);
            while (true)
            {
                BigInteger g = RandomBigInt(BigInteger.One, p - BigInteger.One, random);
                bool isPrimitiveRoot = true;
                foreach (BigInteger factor in factors)
                {
                    BigInteger exp = phi / factor;
                    BigInteger check = BigInteger.ModPow(g, exp, p);
                    if (check == BigInteger.One)
                    {
                        isPrimitiveRoot = false;
                        break;
                    }
                }
                if (isPrimitiveRoot)
                {
                    return g;
                }
            }
        }
        private static BigInteger RandomBigInt(BigInteger min, BigInteger max, Random random)
        {
            BigInteger val;
            do
            {
                byte[] bytes = new byte[max.ToByteArray().Length];
                random.NextBytes(bytes);
                val = new BigInteger(bytes);
            } while (val < min || val > max);
            return val;
        }

        /*private static HashSet<BigInteger> Factors(BigInteger num)
        {
            HashSet<BigInteger> factors = new HashSet<BigInteger>();
            BigInteger factor = 2;
            while (num > BigInteger.One)
            {
                while (num % factor == BigInteger.Zero)
                {
                    factors.Add(factor);
                    num /= factor;
                }
                factor += BigInteger.One;
                if (factor * factor > num)
                {
                    if (num > BigInteger.One)
                    {
                        factors.Add(num);
                    }
                    break;
                }
            }
            return factors;
        }*/
        private static HashSet<BigInteger> Factors(BigInteger num)
        {
            HashSet<BigInteger> factors = new HashSet<BigInteger>();
            if (num % 2 == 0)
            {
                factors.Add(2);
                num /= 2;
            }

            BigInteger factor = 3;
            // Estimate square root using Newton-Raphson
            var estimate = num / 2;
            var lastEstimate = estimate + 1;
            while (estimate < lastEstimate)
            {
                lastEstimate = estimate;
                estimate = (num / estimate + estimate) / 2;
            }
            BigInteger maxFactor = estimate;

            while (num > 1 && factor <= maxFactor)
            {
                while (num % factor == 0)
                {
                    factors.Add(factor);
                    num /= factor;
                    maxFactor = num / factor;
                }
                factor += 2;
            }
            if (num > 2)
            {
                factors.Add(num);
            }
            return factors;
        }



        private BigInteger GenerateK(BigInteger p)
        {
            BigInteger k;
            do
            {
                k = RandomBigInt(2, p-2, random);
            } while (BigInteger.GreatestCommonDivisor(k,p-BigInteger.One) != 1);
            return k;
        }

        private void btnSetKey_Click(object sender, EventArgs e)
        {
            SetKey(false);
        }
        private void SetKey(bool auto)
        {
            if (valA.Text == "" || valAlpha.Text == "" || valBeta.Text == "" || valP.Text == "")
            {
                MessageBox.Show("Please enter all necessary values. (or click Generate key button)","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            if (!BigInteger.TryParse(valA.Text, out a))
            {
                MessageBox.Show("This is a number only field!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!BigInteger.TryParse(valAlpha.Text, out alpha))
            {
                MessageBox.Show("This is a number only field!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!BigInteger.TryParse(valBeta.Text, out beta))
            {
                MessageBox.Show("This is a number only field!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!BigInteger.TryParse(valP.Text, out p))
            {
                MessageBox.Show("This is a number only field!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (auto)
            {
                lblKeySet.Text = "Key automatically set!";
            } else
            {
                if (ValidateKey()) lblKeySet.Text = "Key manually set!"; else
                {
                    MessageBox.Show("Key is not valid. Resetting..","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    Reset();
                    return;
                }
            }
            btnSetKey.Enabled = false;
            btnDecrypt.Enabled = true;
            btnEncrypt.Enabled = true;
        }
        
        private bool ValidateKey()
        {
            if (IsPrime(p) && alpha < p && a > 1 && a <= p - 2)
            {
                if (beta == BigInteger.ModPow(alpha, a, p))
                {
                    return true;
                }
            }
            return false;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            a = alpha = beta = p = 0;
            valA.Text = valAlpha.Text = valBeta.Text = valP.Text = String.Empty;
            btnEncrypt.Enabled = false;
            btnDecrypt.Enabled = false;
            btnSetKey.Enabled = true;
            btnSendToDecrypt.Enabled = false;
            btnSaveCiphertoFile.Enabled = false;
            btnSavePlaintoFile.Enabled = false;
            lblKeySet.Text = "Key not set!";
        }

        private void btnSendToDecrypt_Click(object sender, EventArgs e)
        {
            boxCipherTextInput.Text = boxCipherTextOutput.Text;
        }

        
        private void Encrypt(string message)
        {
            if (message == string.Empty)
            {
                MessageBox.Show("Enter something to encrypt first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            BigInteger k = GenerateK(p);
            BigInteger oneTimeKey = BigInteger.ModPow(beta, k, p);
            BigInteger y1 = BigInteger.ModPow(alpha, k, p);
            byte[] plainTextBytes = Encoding.Unicode.GetBytes(message);
            byte[] y1Bytes = y1.ToByteArray();
            byte[] encrypted = y1Bytes;
            //int count = 0;
            //TODO: clean up this shit
            for (int i = 0; i < plainTextBytes.Length; i += 8)
            {
                int remainingBytes = Math.Min(8, plainTextBytes.Length-i);
                byte[] plainTextBlocks = new byte[remainingBytes];
                Buffer.BlockCopy(plainTextBytes, i, plainTextBlocks, 0, remainingBytes);        
                BigInteger plainText = new BigInteger(plainTextBlocks);
                BigInteger y2 = BigInteger.Remainder(BigInteger.Multiply(plainText,oneTimeKey), p);
                
                byte[] y2Bytes = y2.ToByteArray();
                if (y2Bytes.Length %2 != 0) { y2Bytes = y2Bytes.Append<byte>(0).ToArray(); };
                encrypted = Combine(encrypted, y2Bytes);
                //Console.WriteLine(count++ +". "+Encoding.Unicode.GetString(plainTextBlocks) + " | " + y2 + " | "+ BitConverter.ToString(y2Bytes));
            }
            
            string cipherText = Convert.ToBase64String(encrypted);
            boxCipherTextOutput.Text = cipherText;
            btnSendToDecrypt.Enabled = true;
            btnSaveCiphertoFile.Enabled = true;
            Console.WriteLine();
        }
        private void Decrypt(string message)
        {
            if(message == string.Empty)
            {
                MessageBox.Show("Enter something to decrypt first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string plainText = String.Empty;
            byte[] encrypted = Convert.FromBase64String(message);
            int pLength = p.ToByteArray().Length;
            byte[] y1Bytes = new byte[pLength];
            Buffer.BlockCopy(encrypted, 0, y1Bytes, 0, pLength);
            BigInteger y1 = new BigInteger(y1Bytes);
            BigInteger secret = BigInteger.ModPow(y1, a, p);
            BigInteger secretInverse = BigInteger.ModPow(secret, p - 2, p);
            //int count = 0;
            //TODO: clean up code when done
            //TODO: splice y2 to multiple 8-byte subarrays
            for (int i = pLength; i < encrypted.Length; i+=8)
            {
                int remainingBytes = Math.Min(8,encrypted.Length-i);
                byte[] y2bytes = new byte[remainingBytes];
                Buffer.BlockCopy(encrypted,i,y2bytes,0,remainingBytes);
                BigInteger y2 = new BigInteger(y2bytes);
                //if (y2.Sign == -1) { y2 = BigInteger.Negate(y2); };
                BigInteger decrypted = BigInteger.Remainder(y2 * secretInverse, p);
                byte[] decryptedBytes = decrypted.ToByteArray();
                if (decryptedBytes.Length % 2 != 0) { decryptedBytes = decryptedBytes.Append<byte>(0).ToArray(); }
                plainText += Encoding.Unicode.GetString(decryptedBytes);
                //Console.WriteLine(count++ + ". "+BitConverter.ToString(y2bytes)+" | " +y2 + " | " + Encoding.Unicode.GetString(decryptedBytes));
            }
            boxPlainTextOutput.Text = plainText;
            btnSavePlaintoFile.Enabled = true;
        }

        private void btnReadFromFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Text Document|*.txt",
                Title = "Read plain text from File"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string content = File.ReadAllText(openFileDialog.FileName);
                    boxPlainTextInput.Text = content;
                } catch (Exception ex) {
                    MessageBox.Show($"Exception caught! \nMessage: {ex.Message}","Exception!",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
        }

        private void btnSaveCiphertoFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Text Document|*.txt",
                Title = "Save cipher text to File"
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    File.WriteAllText(saveFileDialog.FileName, boxCipherTextOutput.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Exception caught! \nMessage: {ex.Message}", "Exception!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnReadCipherfromFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Text Document|*.txt",
                Title = "Read cipher text from File"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string content = File.ReadAllText(openFileDialog.FileName);
                    boxCipherTextInput.Text = content;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Exception caught! \nMessage: {ex.Message}", "Exception!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSavePlaintoFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Text Document|*.txt",
                Title = "Save plain text to File"
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    File.WriteAllText(saveFileDialog.FileName, boxPlainTextOutput.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Exception caught! \nMessage: {ex.Message}", "Exception!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}