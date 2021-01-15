#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("/SXGz3KERunO9khhMnaoXqp2d4XxjovPZ22w2qLg3gRnktMBxm56+eqfHYeKQetpunkuSrjCGu1wyjKMpEc1Bf++Qh+T+Kx6V2ofXe8XUUBRtns8ONDPBHhrAWnLtzHUzWPBHiGirKOTIaKpoSGioqM2mDE0Ap+P9u/8+vmJj2SjLb387qvM+0CfY66dj2/kptlqeUZSFLX2ezS5MB1Ot209IuQ9ZmM5ztKMM1xkQt6AU5A8IOv14GkA9FG9dl5UeHSQusolqpQcJHTyly5bCm78CVzdN8K3waqF23+utoO9TzL/RoyfjGVdbzQSm2mofNqSBecfswU894pXP/Hy80ks71iTIaKBk66lqokl6yVUrqKioqajoBloMYXwQ4dbcKGgoqOi");
        private static int[] order = new int[] { 11,5,8,13,12,5,9,7,10,12,10,12,13,13,14 };
        private static int key = 163;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
