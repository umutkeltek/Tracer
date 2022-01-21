using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CreatingCharacters.Abilities
{
    public abstract class Ability : MonoBehaviour
    {
        public abstract IEnumerator Cast();

    }
}

