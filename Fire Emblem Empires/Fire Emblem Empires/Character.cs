using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Trent (Sen) Castro
// Feb. 13, 2017
// Character.cs
// Holds information about a character

namespace Fire_Emblem_Empires
{
    enum Job
    {
        MERCENARY,
        SOLDIER,
        FIGHTER,
        HEALER,
        MAGE,
    }

    class Character
    {
        // Public access methods

        // Private data members
        private byte m_minHealth;
        private byte m_maxHealth;
        private byte m_minAttack;
        private byte m_maxAttack;
        private byte m_minSpeed;
        private byte m_maxSpeed;
        private byte m_minDefense;
        private byte m_maxDefense;
        private byte m_maxResistance;
        private byte m_minResistance;
    };
}
