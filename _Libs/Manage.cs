using UnityEngine;

namespace Management
{
    public abstract class Manage : MonoBehaviour
    {
        private GameObject _fadeobj;
        private int _fadeSiblingIndex;

        protected virtual void Awake()
        {
            _fadeobj = InstantiateUI("Fade", "Canvas", true);
            _fadeSiblingIndex = _fadeobj.transform.GetSiblingIndex();
        }

        public GameObject InstantiateUI(string pn, string cn, bool isfull)
        {
            GameObject resource = (GameObject)Resources.Load(pn);
            GameObject obj = Instantiate(resource, Vector3.zero, Quaternion.identity);
            obj.transform.SetParent(GameObject.Find(cn).transform);

            if (isfull)
                ((RectTransform)obj.transform).offsetMax = new Vector2(0, 0);
            else
                ((RectTransform)obj.transform).anchoredPosition = new Vector2(0, 0);

            if (!pn.Equals("Fade")) obj.transform.SetSiblingIndex(_fadeSiblingIndex);

            return obj;
        }

        public void SetFadeOut(int nextScene)
        {
            if (ManageApp.Inst != null && (ManageApp.Inst.Name == "" || ManageApp.Inst.Name == "none"))
                return;
            if (nextScene != 1 && ManageApp.Inst.gameStartCoin())
                return;
            _fadeobj.GetComponent<Fade>().setNextScene(nextScene);
            _fadeobj.SetActive(true);
            _fadeobj.GetComponent<Fade>().setFadeout();
        }
        public abstract void SetStart();
    }
}