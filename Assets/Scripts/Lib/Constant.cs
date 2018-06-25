using UnityEngine;
using System.Collections;

public enum GAME_STATE {	
	Ready, 
	Round, 
	Gaming, 
	GameEndWait,
	Result		
};
public enum BULLET_KIND {	BULLET, BULLET2, BULLET3, BULLET4	};
public delegate void DelegateVoidFunVoid();

public delegate void VOID_FUNC_VOID();
public delegate void VOID_FUNC_INT(int i);
public delegate void VOID_FUNC_FLOAT(float f);
public delegate void VOID_FUNC_GAMEOBJECT(GameObject _obj);
public delegate void VOID_FUNC_TRANSFORM(Transform _tran);

public class Constant {
	public static readonly Vector3 zero 	= Vector3.zero; 
	public static readonly Vector3 one 		= Vector3.one;
	public static readonly Vector3 forward 	= Vector3.forward;
	public static readonly Vector3 up		= Vector3.up;
	public static readonly Vector3 down		= Vector3.down;

	public static readonly int SPAWNER_MATERIAL_POOL_INIT_COUNT 		= 3;
	public static readonly int BULLETSHELL_MATERIAL_POOL_INIT_COUNT 	= 10;

	public const float BULLET_ALIVE_TIME 		= 2f;
	public const float BULLET_SHELL_POWER 		= 150f;
	public const float BULLET_SHELL_ALIVE_TIME 	= 3f;
	public const float ENEMY_SEARCH_RATE 		= .25f;
	//public const int INT_MAX = int.MaxValue;


	//Turret...
	//public const int WEAPON_ATTACT_ANGLE = 20;

	//GameData
	
}
