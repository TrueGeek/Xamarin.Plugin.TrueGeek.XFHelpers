using System;
using System.Collections.Generic;

namespace TrueGeek.XFHelpers.PubSub
{

    public class Hub<T>
    {

        private readonly List<Action<T>> subscribers = new List<Action<T>>();

        public void Subscribe(Action<T> action)
        {
            subscribers.Add(action);
        }

        public void Publish(T data)
        {

            foreach (var action in subscribers)
            {

                if (action == null)
                {
                    subscribers.Remove(action);
                }
                else
                {
                    action(data);
                }

            }

        }

    }

}
