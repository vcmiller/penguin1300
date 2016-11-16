using UnityEngine;
using System.Collections;
using System;

public class PenguinSlotSaver : StatusSaver {
    public DraggableObject savedOccupant { get; private set; }
    public PenguinSlot slot { get; private set; }

    void Awake() {
        slot = GetComponent<PenguinSlot>();
    }

    public override void Load() {
        slot.resident = savedOccupant;
    }

    public override void Save() {
        savedOccupant = slot.resident;
    }
}
