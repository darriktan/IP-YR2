using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackingQuest : MonoBehaviour
{
    public bool waterPacked;
    public bool ointmentPacked;
    public bool bgauzePacked;
    public bool icepackPacked;
    public bool sbandagePacked;
    public bool disinfectantPacked;
    public bool bandaidboxPacked;
    public bool cgauzePacked;
    public bool cbandagePacked;

    public bool burnPacked;
    public bool sprainPacked;
    public bool cutPacked;
    public bool brokenPacked;

    public void PackedWater()
    {
        waterPacked = true;
        if(waterPacked && ointmentPacked && bgauzePacked)
        {
            burnPacked = true;
        }
    }

    public void PackedOintment()
    {
        ointmentPacked = true;
        if (waterPacked && ointmentPacked && bgauzePacked)
        {
            burnPacked = true;
        }
    }

    public void PackedBurnGauze()
    {
        bgauzePacked = true;
        if (waterPacked && ointmentPacked && bgauzePacked)
        {
            burnPacked = true;
        }
    }

    public void PackedIcePack()
    {
        icepackPacked = true;
        if (icepackPacked && sbandagePacked)
        {
            sprainPacked = true;
        }
    }

    public void PackedSprainBandage()
    {
        sbandagePacked = true;
        if (icepackPacked && sbandagePacked)
        {
            sprainPacked = true;
        }
    }

    public void PackedDisinfectant()
    {
        disinfectantPacked = true;
        if (disinfectantPacked && bandaidboxPacked && cgauzePacked && cbandagePacked)
        {
            cutPacked = true;
        }
    }

    public void PackedBandAidBox()
    {
        bandaidboxPacked = true;
        if (disinfectantPacked && bandaidboxPacked && cgauzePacked && cbandagePacked)
        {
            cutPacked = true;
        }
    }

    public void PackedCutGauze()
    {
        cgauzePacked = true;
        if (disinfectantPacked && bandaidboxPacked && cgauzePacked && cbandagePacked)
        {
            cutPacked = true;
        }
    }

    public void PackedCutBandage()
    {
        cbandagePacked = true;
        if (disinfectantPacked && bandaidboxPacked && cgauzePacked && cbandagePacked)
        {
            cutPacked = true;
        }
    }

    public void PackedTriBandage()
    {
        brokenPacked = true;
    }

    public void UnpackedWater()
    {
        waterPacked = false;
        burnPacked = false;
    }

    public void UnpackedOintment()
    {
        ointmentPacked = false;
        burnPacked = false;
    }

    public void UnpackedBurnGauze()
    {
        bgauzePacked = false;
        burnPacked = false;
    }

    public void UnpackedIcePack()
    {
        icepackPacked = false;
        sprainPacked = false;
    }

    public void UnpackedSprainBandage()
    {
        sbandagePacked = false;
        sprainPacked = false;
    }   

    public void UnpackedDisinfectant()
    {
        disinfectantPacked = false;
        cutPacked = false;
    }

    public void UnpackedBandAidBox()
    {
        bandaidboxPacked = false;
        cutPacked = false;
    }

    public void UnpackedCutGauze()
    {
        cgauzePacked = false;
        cutPacked = false;
    }

    public void UnpackedCutBandage()
    {
        cbandagePacked = false;
        cutPacked = false;
        
    }

    public void UnpackedTriBandage()
    {
        brokenPacked = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
