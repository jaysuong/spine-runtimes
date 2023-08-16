using System.Collections.Generic;
using UnityEngine;

namespace Spine.Unity
{
    public interface ISkeletonUpdate
    {
        public void Update(in float unscaledDeltaTime, in float deltaTime);
    }

    public class SpineAnimatorUpdater : MonoBehaviour
    {

        private static List<ISkeletonUpdate>[] UpdateLists = new List<ISkeletonUpdate> {
            new List<ISkeletonUpdate>(16),
            new List<ISkeletonUpdate>(16)
        };

        private enum UpdateType { Update, FixedUpdate }


        private void FixedUpdate()
        {
            var list = UpdateLists[(int)UpdateType.FixedUpdate];
            var unscaledDt = Time.unscaledDeltaTime;
            var scaledDt = Time.deltaTime;

            for (var i = 0; i < list.Count; ++i)
            {
                list[i].Update(unscaledDt, scaledDt);
            }
        }

        private void Update()
        {
            var list = UpdateLists[(int)UpdateType.Update];
            var unscaledDt = Time.unscaledDeltaTime;
            var scaledDt = Time.deltaTime;

            for (var i = 0; i < list.Count; ++i)
            {
                list[i].Update(unscaledDt, scaledDt);
            }
        }

        public static void RegisterUpdate(ISkeletonUpdate reference)
        {
            UpdateLists[(int)UpdateType.Update].Add(reference);
        }

        public static void UnregisterUpdate(ISkeletonUpdate reference)
        {
            UpdateLists[(int)UpdateType.Update].Remove(reference);
        }

        public static void RegisterFixedUpdate(ISkeletonUpdate reference)
        {
            UpdateLists[(int)UpdateType.FixedUpdate].Add(reference);
        }

        public static void UnregisterFixedUpdate(ISkeletonUpdate reference)
        {
            UpdateLists[(int)UpdateType.FixedUpdate].Remove(reference);
        }
    }
}