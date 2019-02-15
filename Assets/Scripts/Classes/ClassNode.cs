using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class ClassNode
{
    [SerializeField]
    protected static int whenToUpgrade;
    //[SerializeField]
    //private string className;
    //[SerializeField]
    //private ClassNode parentClass;
    //[SerializeField]
    //private ClassNode[] childClasses;

    //public void InitNode(ClassNode parentClass, ClassNode[] childClasses){
    //    this.parentClass = parentClass;
    //    this.childClasses = childClasses;
    //}

    //static ClassNode AttachChild(ClassNode parent, ClassNode child){
    //    List<ClassNode> children = new List<ClassNode>(parent.GetChildren());
    //    if (!children.Contains(child)) {
    //      children.Add(child);
    //    }
    //    parent.SetChildren(children.ToArray());
    //    return parent;
    //}

    public abstract ClassNode GetParent();
    
    public abstract ClassNode[] GetChildren();

    //public ClassNode GetParent(){
    //    return parentClass;
    //}

    //public void SetChildren(ClassNode parentClass){
    //    this.parentClass = parentClass;
    //}

    //public ClassNode[] GetChildren(){
    //    return childClasses;
    //}

    //public void SetChildren(ClassNode[] childClasses){
    //    this.childClasses = childClasses;
    //}

    //public static ClassNode CreateClassNode(ClassNode parent, ClassNode cNode){
    //    ClassNode node = cNode;
    //    node.InitNode(parent, new ClassNode[]{ });
    //    return node;
    //}

    public static ClassNode ComputeClassObject(Unit.FactionType fType, Unit.UnitType uType){
        ClassNode rNode = new HumanBaseSoldier();
        switch(fType) {
          case Unit.FactionType.Human: 
          switch(uType){
            case Unit.UnitType.Mage: return new HumanBaseMage();
            case Unit.UnitType.Scout: return new HumanBaseScout();
            case Unit.UnitType.Soldier: return new HumanBaseSoldier();
          }
          break;
        }
        return new HumanBaseSoldier();
    }

    public abstract Unit UpgradeCharacter(Unit unit);
    public string ClassDescFull(){
      return ClassName() + ": " + ClassDesc();
    }
    public abstract string ClassDesc();
    public abstract string ClassName();
    public int GetWhenToUpgrade(){
        return whenToUpgrade;
    }
}
