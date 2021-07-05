using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.StructuralPatterns.Decorator
{
    public interface IBird
    {
        void Fly();
    }
    public class Bird : IBird
    {
        public void Fly()
        {

        }
    }
    public interface ILizard
    {
        void Crawl();
    }
    public class Lizard: ILizard
    {
        public void Crawl()
        {

        }
    }

    //Dragon is a Decorator class for implementing the functionality for  component Bird and Lizard
    public class Dragon :IBird,ILizard // no multiple inheritance
    {
        private IBird bird;
        private ILizard lizard;

        public Dragon(IBird bird, ILizard lizard)
        {
            this.bird = bird ?? throw new ArgumentNullException(paramName: nameof(bird));
            this.lizard = lizard ?? throw new ArgumentNullException(paramName: nameof(lizard));
        }

        public void Crawl()
        {
            lizard.Crawl();
        }

        public void Fly()
        {
            bird.Fly();
        }
    }
}
