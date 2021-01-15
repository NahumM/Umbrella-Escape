#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class AppleTangle
    {
        private static byte[] data = System.Convert.FromBase64String("PZFfkeAaFhYSEhcndSYcJx4RFEKXAzzHflCDYR7p43yaOVex4FBaaCeVE6wnlRS0txQVFhUVFhUnGhEeeXM3dHh5c35jfnh5ZDd4cTdiZHI3dnlzN3RyZWN+cX50dmN+eHk3Z2d7cjdFeHhjN1RWJwkAGichJyMlv8tpNSLdMsLOGMF8w7UzNAbgtruCiW0bs1CcTMMBICTc0xha2QN+xiQhTSd1JhwnHhEUQhMRBBVCRCYECIbMCVBH/BL6SW6TOvwhtUBbQvs5V7HgUFpoH0knCBEUQgo0Ew8nAXMiNAJcAk4KpIPg4YuJ2Eet1k9HJwYRFEITHQQdVmdne3I3Xnl0OSY7N3RyZWN+cX50dmNyN2d4e350blJpCFt8R4FWntNjdRwHlFaQJJ2WpidP+00TJZt/pJgKyXJk6HBJcqs3eHE3Y39yN2N/cnk3dmdne350dnV7cjdkY3Z5c3ZlczdjcmV6ZDd2ERRCChkTARMDPMd+UINhHunjfJoBJwMRFEITFAQaVmdne3I3RXh4Y84haNaQQs6wjq4lVezPwmaJabZFmGSWd9EMTB44haXvU1/ndy+JAuK8tGaFUERC1rg4VqTv7PRn2vG0W3CYH6M34Ny7Ozd4Z6EoFieboFTYY35xfnR2Y3I3dW43dnluN2d2ZWMiJSYjJyQhTQAaJCInJScuJSYjJ5UWFxEePZFfkeB0cxIWJ5blJz0RZXZ0Y350cjdkY3ZjcnpyeWNkOScYiirkPF4/Dd/p2aKuGc5JC8HcKntyN155dDkmMSczERRCExwEClZnnA6eye5ce+IQvDUnFf8PKe9HHsQTEQQVQkQmBCcGERRCEx0EHVZnZxoRHj2RX5HgGhYWEhIXFJUWFhdLfnF+dHZjfnh5N1ZiY394ZX5jbiaiLbrjGBkXhRymNgE5Y8IrGsx1ARD7ai6UnEQ3xC/TpqiNWB186Dzr3g5l4koZwmhIjOUyFK1CmFpKGuYz9fzGoGfIGFL2MN3mem/68KIAAEVye352eXRyN3h5N2N/fmQ3dHJlbSeVFmEnGREUQgoYFhboExMUFRYxJzMRFEITHAQKVmdne3I3VHJlYx9JJ5UWBhEUQgo3E5UWHyeVFhMnY394ZX5jbiYBJwMRFEITFAQaVmduN3ZkZGJ6cmQ3dnR0cmdjdnl0cl7PYYgkA3K2YIPeOhUUFhcWtJUWOCeW1BEfPBEWEhIQFRUnlqENlqRne3I3VHJlY35xfnR2Y354eTdWYtd0JGDgLRA7QfzNGDYZza1kDliiYGA5dmdne3I5dHh6OHZnZ3tydHZoVr+P7sbdcYszfAbHtKzzDD3UCB88ERYSEhAVFgEJf2NjZ2QtODhgqeNkjPnFcxjcblgjz7Up7m/ofN8qMXA3nSR94BqV2Mn8tDjuRH1Mc6AMqoRVMwU90BgKoVqLSXTfXJcACJKUkgyOKlAg5b6MV5k7w6aHBc8hjls6b6D6m4zL5GCM5WHFYCdY1hEnGBEUQgoEFhboExInFBYW6CcKEhcUlRYYFyeVFh0VlRYWF/OGvh5OsBIeawBXQQYJY8SgnDQsULTCeDdUVieVFjUnGhEePZFfkeAaFhYWR72dws3z68ceECCnYmI2");
        private static int[] order = new int[] { 1,32,32,43,45,6,54,18,8,15,31,20,15,44,25,26,41,42,30,26,58,55,24,55,39,30,34,46,39,43,41,32,47,58,55,51,55,53,38,39,47,58,54,47,46,53,53,57,55,54,51,51,56,59,54,55,57,58,58,59,60 };
        private static int key = 23;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
