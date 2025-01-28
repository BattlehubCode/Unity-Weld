using UnityEngine;
using UnityEngine.Assertions;

namespace Weld.Binding
{
    /// <summary>
    /// Template for use in collection bindings.
    /// </summary>
    public interface ITemplate
    {
        /// <summary>
        /// Set the view model and initialise all binding objects down the hierarchy.
        /// </summary>
        void InitChildBindings(object viewModel);
    }

    /// <summary>
    /// Template for use in collection bindings.
    /// </summary>
    [AddComponentMenu("Unity Weld/Template")]
    [HelpURL("https://github.com/Real-Serious-Games/Unity-Weld")]
    public class Template : MonoBehaviour, IViewModelProvider, ITemplate
    {
        /// <summary>
        /// Get the view-model provided by this provider.
        /// </summary>
        public object GetViewModel()
        {
            return viewModel;
        }

        /// <summary>
        /// Get the name of the view-model's type.
        /// </summary>
        public string GetViewModelTypeName()
        {
            return viewModelTypeName;
        }

        public string ViewModelTypeName
        {
            get { return viewModelTypeName; }
            set { viewModelTypeName = value; }
        }

        [SerializeField]
        private string viewModelTypeName = string.Empty;

        /// <summary>
        /// Cached view-model object.
        /// </summary>
        private object viewModel;

        private void OnEnable()
        {
            if (viewModel != null)
            {
                InitChildBindings(viewModel);
            }
        }

        private void OnDisable()
        {
            if(viewModel != null)
            {
                DisconnectChildBindings();
            }
        }

        private void OnDestroy()
        {
            viewModel = null;
        }

        /// <summary>
        /// Set the view model and initialise all binding objects down the hierarchy.
        /// </summary>
        public void InitChildBindings(object viewModel)
        {
            Assert.IsNotNull(viewModel, "Cannot initialise child bindings with null view model.");

            // Set the bound view to the new view model.
            this.viewModel = viewModel;

            if (gameObject.activeInHierarchy) 
            {
                // GetComponentsInChildren doesn't do what you might expect in this case.
                // https://forum.unity.com/threads/getcomComponentsinchildren-false-returns-inactive-objects.501177/

                // Call GetComponentsInChildren and binding.Init() child objects only if the current game object is active in the hierarchy
                // If you called Binding.Init on an inactive object and it never Awake during its lifecycle (OnDestroy will not be called)
                // the binding will never be disconnected.

                foreach (var binding in GetComponentsInChildren<AbstractMemberBinding>())
                {
                    binding.Init();
                }
            }
        }

        private void DisconnectChildBindings()
        {
            foreach (var binding in GetComponentsInChildren<AbstractMemberBinding>())
            {
                binding.Disconnect();
            }
        }
    }
} 