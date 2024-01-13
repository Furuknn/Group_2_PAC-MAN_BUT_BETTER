using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class SpriteSwitcher : MonoBehaviour
{
    [SerializeField] private SpriteRenderer classicBody, classicEyes, classicBlueMode, classicWhiteMode;
    [SerializeField] private SpriteRenderer betterBody, betterEyes, betterBlueMode, betterWhiteMode;

    private GhostVulnerable ghostVulnerablescr;

    private void Awake()
    {
        ghostVulnerablescr = GetComponent<GhostVulnerable>();

        if (PlayerPrefs.GetString("pacmanVersion") == "classic")
        {
            ghostVulnerablescr.body = classicBody;
           ghostVulnerablescr.eyes = classicEyes;
            ghostVulnerablescr.blueMode = classicBlueMode;
            ghostVulnerablescr.whiteMode = classicWhiteMode;
        }
        else
        {
            ghostVulnerablescr.body = betterBody;
            ghostVulnerablescr.eyes = betterEyes;
            ghostVulnerablescr.blueMode = betterBlueMode;
            ghostVulnerablescr.whiteMode = betterWhiteMode;
        }
    }
}
