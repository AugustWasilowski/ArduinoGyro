
using System.Collections;

namespace Assets.Scripts
{
    public class ThreadedJob
    {
        private bool isDone = false;
        private object handle = new object();

        private System.Threading.Thread thread = null;

        public bool IsDone
        {
            get
            {
                bool tmp;
                lock (this.handle)
                {
                    tmp = this.isDone;
                }

                return tmp;
            }
            set
            {
                lock (this.handle)
                {
                    isDone = value;
                }
            }
        }

        public virtual void Start()
        {
            this.thread = new System.Threading.Thread(Run);
            this.thread.Start();
        }

        public virtual void Abort()
        {
            this.thread.Abort();
        }

        protected virtual void ThreadFunction()
        {
            
        }

        protected virtual void OnFinished()
        {

        }

        public virtual bool Update()
        {
            if (this.IsDone)
            {
                this.OnFinished();
                return true;
            }

            return false;
        }

        IEnumerator WaitFor()
        {
            while (!Update())
            {
                yield return null;
            }
        }

        private void Run()
        {
            this.ThreadFunction();
            this.IsDone = true;
        }
    }
}
