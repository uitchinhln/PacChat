
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChat.MVC
{
    public class Entity
    {
        public App app
        {
            get
            {
                return m_app = Assert<App>(m_app);
            }
        }
        private App m_app;

        public T Assert<T>(T p_var) where T : App
        {
            return p_var == null ? (T)AppManager.GetAppOfType<T>() : p_var;
        }
    }

    public class Entity<T> : Entity where T : App
    {
        new public T app
        {
            get
            {
                return (T)base.app;
            }
        }
    }

    public class App : Entity
    {
        public Model model { get; private set; }

        public View view { get; private set; }

        public Controller controller { get; private set; }

        public void InitializeMVC(Model model, View view, Controller controller)
        {
            this.model = model;
            this.view = view;
            this.controller = controller;
        }
    }

    public class App<M, V, C> : App
        where M : Model
        where V : View
        where C : Controller
    {
        new public M model { get; private set; }
        new public V view { get; private set; }
        new public C controller { get; private set; }

        public void InitializeMVC(M model, V view, C controller)
        {
            this.model = model;
            this.view = view;
            this.controller = controller;

            AppManager.Register(this);
        }

        ~App()
        {
            AppManager.Unregister(this);
        }
    }
}
