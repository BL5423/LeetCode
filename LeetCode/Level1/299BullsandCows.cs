using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level1
{
    public class _299BullsandCows
    {
        public string GetHint(string secret, string guess)
        {
            int[] count = new int['9' - '0' + 1];
            int bulls = 0, cows = 0;
            for(int i = 0; i < secret.Length; ++i)
            {
                int s = secret[i] - '0';
                int g = guess[i] - '0';
                if (s == g)
                {
                    ++bulls;
                }
                else
                {
                    // there was a match from guess previously that caused count[guess[k]] less than 0, guess[k] == s
                    if (count[s] < 0)
                        ++cows;

                    // there was a match from secret previously that caused count[secret[j]] greater than 0, secret[j] == g
                    if (count[g] > 0)
                        ++cows;

                    // remember that there is a potential cow from guess by decreasing 1, it has another effect:
                    // remove the match(if any) above from secret, so no double-couting.
                    --count[g];

                    // remember that there is a potential cow from secret by increasing 1, it has another effect:
                    // remove the match(if any) above from guess, so no double-counting.
                    ++count[s];
                }
            }

            return string.Format("{0}A{1}B", bulls, cows);
        }

        public string GetHintV1(string secret, string guess)
        {
            Dictionary<char, int> countsInSecret = new Dictionary<char, int>(secret.Length);
            Dictionary<char, int> countsInGuess = new Dictionary<char, int>(guess.Length);
            int bulls = 0, cows = 0;
            for(int i = 0; i < secret.Length; ++i)
            {
                if (secret[i] == guess[i])
                {
                    ++bulls;
                    continue;
                }

                if (countsInSecret.TryGetValue(secret[i], out int count1))
                {
                    ++countsInSecret[secret[i]];
                }
                else
                {
                    countsInSecret[secret[i]] = 1;
                }
                if (countsInGuess.TryGetValue(guess[i], out int count2))
                {
                    ++countsInGuess[guess[i]];
                }
                else
                {
                    countsInGuess[guess[i]] = 1;
                }

                if (countsInSecret.TryGetValue(guess[i], out int count3) && count3 > 0)
                {
                    ++cows;
                    --countsInSecret[guess[i]];
                    --countsInGuess[guess[i]];
                }
                if (countsInGuess.TryGetValue(secret[i], out int count4) && count4 > 0)
                {
                    ++cows;
                    --countsInGuess[secret[i]];
                    --countsInSecret[secret[i]];
                }
            }

            return string.Format("{0}A{1}B", bulls, cows);
        }
    }
}
