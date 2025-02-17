using MSCLoader;
using UnityEngine;

namespace MSCPeugeot301
{
    public class Peugeot301 : Mod
    {
        public override string ID => "MSCPeugeot301";
        public override string Name => "Peugeot 301";
        public override string Author => "exyxz";
        public override string Version => "0.1-concept2";
        public override string Description => "An actual Peugeot 301 for Satsuma.";
        public override bool UseAssetsFolder => true;

        public override void ModSetup()
        {
            SetupFunction(Setup.PostLoad, Mod_PostLoad);
        }

        public void SwapMirror(AssetBundle bundle, GameObject go, Material[] mirrors, Camera cam)
        {
            MeshFilter filter = go.GetComponent<MeshFilter>();
            filter.mesh = bundle.LoadAsset<Mesh>("fullsize_mirror.fbx");
            MeshRenderer renderer = filter.GetComponent<MeshRenderer>();
            renderer.materials = mirrors;
            renderer.materials[2].mainTexture = cam.targetTexture;
        }

        private void Mod_PostLoad()
        {
            AssetBundle bundle = LoadAssets.LoadBundle("MSCPeugeot301.Assets.peugeot");
            MeshFilter[] filters = Object.FindObjectsOfType<MeshFilter>();
            GameObject body = bundle.LoadAsset<GameObject>("body.prefab");
            Material chrome = bundle.LoadAsset<Material>("super_chrome-material.mat");
            foreach (MeshFilter filter in filters)
            {
                switch (filter.name)
                {
                    case "car body masse(xxxxx)":
                        filter.gameObject.SetActive(false);
                        break;

                    case "car body(xxxxx)":
                        filter.mesh = body.transform.GetChild(0).GetChild(0).GetComponent<MeshFilter>().mesh;
                        filter.GetComponent<MeshRenderer>().materials = body.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().materials;
                        GameObject b2 = new GameObject("doorRL");
                        b2.AddComponent<MeshFilter>().mesh = body.transform.GetChild(1).GetComponent<MeshFilter>().mesh;
                        b2.AddComponent<MeshRenderer>().material = body.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material;
                        b2.transform.parent = filter.transform;
                        b2.transform.position = filter.transform.position;
                        b2.transform.rotation = filter.transform.rotation;
                        GameObject b3 = new GameObject("doorRR");
                        b3.AddComponent<MeshFilter>().mesh = body.transform.GetChild(2).GetComponent<MeshFilter>().mesh;
                        b3.AddComponent<MeshRenderer>().material = body.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material;
                        b3.transform.parent = filter.transform;
                        b3.transform.position = filter.transform.position;
                        b3.transform.rotation = filter.transform.rotation;
                        break;

                    case "hood(Clone)":
                        filter.mesh = bundle.LoadAsset<Mesh>("fullsize_hood.fbx");
                        filter.GetComponent<MeshRenderer>().materials = body.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().materials;
                        break;

                    case "bootlid(Clone)":
                        filter.mesh = bundle.LoadAsset<Mesh>("fullsize_trunk.fbx");
                        filter.GetComponent<MeshRenderer>().materials = new Material[2] { chrome, bundle.LoadAsset<Material>("siyah_k.6.mat") };
                        break;

                    case "door left(Clone)":
                        filter.mesh = bundle.LoadAsset<Mesh>("fullsize_fdoor.fbx");
                        filter.GetComponent<MeshRenderer>().material = body.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material;
                        break;

                    case "door right(Clone)":
                        filter.mesh = bundle.LoadAsset<Mesh>("fullsize_fdoorr.fbx");
                        filter.GetComponent<MeshRenderer>().material = body.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material;
                        break;

                    case "bumper front(Clone)":
                        filter.mesh = body.transform.GetChild(3).GetComponent<MeshFilter>().mesh;
                        filter.GetComponent<MeshRenderer>().materials = body.transform.GetChild(3).GetComponent<MeshRenderer>().materials;
                        break;

                    case "bumper rear(Clone)":
                        filter.mesh = body.transform.GetChild(4).GetComponent<MeshFilter>().mesh;
                        filter.GetComponent<MeshRenderer>().materials = body.transform.GetChild(4).GetComponent<MeshRenderer>().materials;
                        break;

                    case "headlight left(Clone)":
                    case "headlight right(Clone)":
                        filter.GetComponent<MeshRenderer>().enabled = false;
                        filter.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = false;
                        break;

                    case "rearlight(leftx)":
                    case "rearlight(right)":
                        filter.GetComponent<MeshRenderer>().enabled = false;
                        filter.transform.GetChild(0).gameObject.SetActive(false);
                        break;

                    case "fender left(Clone)":
                    case "fender right(Clone)":
                        filter.GetComponent<MeshRenderer>().enabled = false;
                        break;
                }
            }
            GameObject crap = GameObject.Find("SATSUMA(557kg, 248)/MiscParts/radiator(xxxxx)");
            if (crap) crap.transform.position -= Vector3.up * .1f;
            GameObject.Find("SATSUMA(557kg, 248)/MiscParts/trigger_radiator").transform.position -= Vector3.up * .1f;
            GameObject.Find("SATSUMA(557kg, 248)/MiscParts/trigger_battery").transform.position -= GameObject.Find("SATSUMA(557kg, 248)").transform.forward * .2f;
            crap = GameObject.Find("SATSUMA(557kg, 248)/MiscParts/pivot_headlight_left");
            if (crap) crap.transform.localPosition = new Vector3(.53f, .654f, -1.525f);
            crap = GameObject.Find("SATSUMA(557kg, 248)/MiscParts/pivot_headlight_right");
            if (crap) crap.transform.localPosition = new Vector3(-.53f, .654f, -1.525f);
            Material[] mirror_materials = new Material[3] { chrome, chrome, bundle.LoadAsset<Material>("shine.mat") };
            Camera mirrorCamera = GameObject.Find("Systems/Mirrors/Disable/LeftSideMirrorCam").GetComponent<Camera>();
            crap = GameObject.Find("SATSUMA(557kg, 248)/Body/pivot_door_left/door left(Clone)/mirror");
            if (crap) SwapMirror(bundle, crap, mirror_materials, mirrorCamera);
            crap = GameObject.Find("SATSUMA(557kg, 248)/Body/pivot_door_right/door right(Clone)/mirror(Clone)");
            if (crap)
            {
                crap.transform.localScale = new Vector3(-1, 1, 1);
                SwapMirror(bundle, crap, mirror_materials, mirrorCamera);
            }
            bundle.Unload(false);
        }
    }
}
