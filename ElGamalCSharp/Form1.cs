using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Numerics;
using System.IO;
using Microsoft.Office.Interop.Word;

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
        private readonly bool PowerUser = false;
        public Form1()
        {
            InitializeComponent();
            btnEncrypt.Enabled = false;
            btnDecrypt.Enabled = false;
            btnSendToDecrypt.Enabled = false;
            btnSaveCiphertoFile.Enabled = false;
            btnSavePlaintoFile.Enabled = false;
            btnExportKeys.Enabled = false;
            if (!PowerUser)
            {
                btnReset.Hide();
                btnSetKey.Hide();
                btnExportKeys.Hide();
                btnImportKeys.Hide();
                btnGenerateKey.Hide();
                lblKeySet.Hide();
                label1.Hide();
                label2.Hide();
                label3.Hide();
                label4.Hide();
                valA.Hide();
                valAlpha.Hide();
                valBeta.Hide();
                valP.Hide();
                GenerateKeys();
                SetKey(true);
            }
        }


        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            Encrypt(boxPlainTextInput.Rtf);
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            Decrypt(boxCipherTextInput.Text);
        }

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
            BigInteger vala = RandomBigInt(BigInteger.One, valp - BigInteger.One, random);
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
            btnExportKeys.Enabled = true;
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

        private static int[] _primes = GeneratePrimes(10000000); // Precomputed list of primes up to 10000000

        public static HashSet<BigInteger> Factors(BigInteger n)
        {
            HashSet<BigInteger> factors = new HashSet<BigInteger>();

            // Check for small prime factors
            while (n % 2 == 0)
            {
                factors.Add(2);
                n /= 2;
            }

            // Check for larger prime factors
            foreach (int p in _primes)
            {
                if (n <= 1)
                    break;
                while (n % p == 0)
                {
                    factors.Add(p);
                    n /= p;
                }
            }

            // If there's anything left of n, it's a prime factor
            if (n > 1)
                factors.Add(n);

            return factors;
        }

        private static int[] GeneratePrimes(int limit)
        {
            bool[] isComposite = new bool[limit + 1];
            HashSet<int> primes = new HashSet<int>();
            for (int i = 2; i <= limit; i++)
            {
                if (!isComposite[i])
                {
                    primes.Add(i);
                    for (int j = 2 * i; j <= limit; j += i)
                        isComposite[j] = true;
                }
            }
            return primes.ToArray();
        }


        private BigInteger GenerateK(BigInteger p)
        {
            BigInteger k;
            do
            {
                k = RandomBigInt(2, p - 2, random);
            } while (BigInteger.GreatestCommonDivisor(k, p - BigInteger.One) != 1);
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
                MessageBox.Show("Please enter all necessary values. (or click Generate key button)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            }
            else
            {
                if (ValidateKey()) lblKeySet.Text = "Key manually set!";
                else
                {
                    MessageBox.Show("Key is not valid. Resetting..", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Reset();
                    return;
                }
            }
            btnSetKey.Enabled = false;
            btnDecrypt.Enabled = true;
            btnEncrypt.Enabled = true;
            btnExportKeys.Enabled = true;
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
            btnExportKeys.Enabled = false;
            lblKeySet.Text = "Key not set!";
        }

        private void btnSendToDecrypt_Click(object sender, EventArgs e)
        {
            boxCipherTextInput.Rtf = boxCipherTextOutput.Rtf;
        }


        private void Encrypt(string message)
        {
            if (message == string.Empty)
            {
                MessageBox.Show("Enter something to encrypt first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int pLength = p.ToByteArray().Length;
            BigInteger k = GenerateK(p);
            BigInteger oneTimeKey = BigInteger.ModPow(beta, k, p);
            BigInteger y1 = BigInteger.ModPow(alpha, k, p);
            byte[] plainTextBytes = Encoding.Unicode.GetBytes(message);
            byte[] y1Bytes = y1.ToByteArray();
            byte[] encrypted = y1Bytes;
            for (int i = 0; i < plainTextBytes.Length; i += pLength)
            {
                int remainingBytes = Math.Min(pLength, plainTextBytes.Length - i);
                byte[] plainTextBlocks = new byte[remainingBytes];
                Buffer.BlockCopy(plainTextBytes, i, plainTextBlocks, 0, remainingBytes);
                BigInteger plainText = new BigInteger(plainTextBlocks);
                BigInteger y2 = BigInteger.Remainder(BigInteger.Multiply(plainText, oneTimeKey), p);

                byte[] y2Bytes = y2.ToByteArray();
                if (y2Bytes.Length % 2 != 0) { y2Bytes = y2Bytes.Append<byte>(0).ToArray(); };
                encrypted = Combine(encrypted, y2Bytes);
            }

            string cipherText = Convert.ToBase64String(encrypted);
            boxCipherTextOutput.Text = cipherText;
            btnSendToDecrypt.Enabled = true;
            btnSaveCiphertoFile.Enabled = true;
        }
        private void Decrypt(string message)
        {
            if (message == string.Empty)
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
            for (int i = pLength; i < encrypted.Length; i += pLength)
            {
                int remainingBytes = Math.Min(pLength, encrypted.Length - i);
                byte[] y2bytes = new byte[remainingBytes];
                Buffer.BlockCopy(encrypted, i, y2bytes, 0, remainingBytes);
                BigInteger y2 = new BigInteger(y2bytes);
                BigInteger decrypted = BigInteger.Remainder(y2 * secretInverse, p);
                byte[] decryptedBytes = decrypted.ToByteArray();
                if (decryptedBytes.Length % 2 != 0) { decryptedBytes = decryptedBytes.Append<byte>(0).ToArray(); }
                plainText += Encoding.Unicode.GetString(decryptedBytes);
            }
            boxPlainTextOutput.Rtf = plainText;
            btnSavePlaintoFile.Enabled = true;
        }

        private void btnReadFromFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Text Document|*.txt|Microsoft Word Document|*.docx",
                Title = "Read plain text from File"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    boxPlainTextInput.Clear();
                    string filePath = openFileDialog.FileName;
                    string fileExtension = Path.GetExtension(filePath);
                    if (fileExtension == ".txt")
                    {
                        string content = File.ReadAllText(openFileDialog.FileName);
                        boxPlainTextInput.Text = content;
                    }
                    if (fileExtension == ".docx")
                    {
                        Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                        Document document = wordApp.Documents.Open(filePath);
                        document.Content.Copy();
                        boxPlainTextInput.Paste();
                        document.Close();
                        wordApp.Quit();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Exception caught! \nMessage: {ex.Message}", "Exception!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSaveCiphertoFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Text Document|*.txt|Microsoft Word Document|*.docx",
                Title = "Save cipher text to File"
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string fileName = saveFileDialog.FileName;
                    string fileExtension = Path.GetExtension(fileName);
                    if (fileExtension == ".txt")
                    {
                        File.WriteAllText(saveFileDialog.FileName, boxCipherTextOutput.Text);
                    }
                    if (fileExtension == ".docx")
                    {
                        Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                        Document document = wordApp.Documents.Add();
                        document.Content.Text = boxCipherTextOutput.Text;
                        document.SaveAs(fileName);
                        document.Close();
                        wordApp.Quit();
                    }
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
                Filter = "Text Document|*.txt|Microsoft Word Document|*.docx",
                Title = "Read cipher text from File"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    boxCipherTextInput.Clear();
                    string filePath = openFileDialog.FileName;
                    string fileExtension = Path.GetExtension(filePath);
                    if (fileExtension == ".txt")
                    {
                        string content = File.ReadAllText(openFileDialog.FileName);
                        boxCipherTextInput.Text = content;
                    }
                    if (fileExtension == ".docx")
                    {
                        Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                        Document document = wordApp.Documents.Open(filePath);
                        document.Content.Copy();
                        boxCipherTextInput.Paste();
                        document.Close();
                        wordApp.Quit();
                        
                    }
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
                Filter = "Text Document|*.txt|Microsoft Word Document|*.docx",
                Title = "Save plain text to File"
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string fileName = saveFileDialog.FileName;
                    string fileExtension = Path.GetExtension(fileName);
                    if (fileExtension == ".txt")
                    {
                        File.WriteAllText(saveFileDialog.FileName, boxPlainTextOutput.Text);
                    }
                    if (fileExtension == ".docx")
                    {
                        Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                        byte[] RTFdata = Encoding.UTF8.GetBytes(boxPlainTextOutput.Rtf);
                        string tempPath = Path.Combine(Path.GetTempPath(), "temp.rtf");
                        File.WriteAllBytes(tempPath, RTFdata);
                        Document document = wordApp.Documents.Open(tempPath);
                        document.SaveAs2(fileName,WdSaveFormat.wdFormatDocumentDefault);
                        document.Close();
                        wordApp.Quit();
                        File.Delete(tempPath);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Exception caught! \nMessage: {ex.Message}", "Exception!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnExportKeys_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Text Document|*.txt",
                Title = "Save encryption keys to File"
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    File.WriteAllText(saveFileDialog.FileName, valAlpha.Text + " " + valBeta.Text + " " + valP.Text + " " + valA.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Exception caught! \nMessage: {ex.Message}", "Exception!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnImportKeys_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Text Document|*.txt",
                Title = "Read encryption keys from File"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string content = File.ReadAllText(openFileDialog.FileName);
                    string[] keys = content.Split(' ');
                    valAlpha.Text = keys[0];
                    valBeta.Text = keys[1];
                    valP.Text = keys[2];
                    valA.Text = keys[3];
                    SetKey(false);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Exception caught! \nMessage: {ex.Message}", "Exception!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
