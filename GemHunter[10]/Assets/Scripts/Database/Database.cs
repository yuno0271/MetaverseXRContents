using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class Database
{
	// 플레이어의 데이터를 저장/불러오는 파일 이름
	public static readonly string	DBFileName = "Database.dat";
	public static readonly int		maxChapter = 3;

	// 게임에서 사용하는 모든 데이터를 저장하는 클래스
	public static DatabaseItem		DBItem	 { get; private set; } = new DatabaseItem();
	public static bool				IsReaded { get; private set; } = false;

	/// <summary>
	/// 파일에 데이터를 저장
	/// </summary>
	public static void Write()
	{
		Logger.Log("Database::Write - Save data in Database");

		string path = Path.Combine(Application.persistentDataPath, DBFileName);
		
		Stream			stream		= new FileStream(path, FileMode.Create);
		BinaryFormatter serializer	= new BinaryFormatter();

		serializer.Serialize(stream, DBItem);

		stream.Position = 0;
		stream.Close();
	}

	/// <summary>
	/// 파일로부터 데이터를 불러옴
	/// </summary>
	public static void Read()
	{
		Logger.Log("Database::Read - Load data in Database");
		
		IsReaded = true;

		string path = Path.Combine(Application.persistentDataPath, DBFileName);

		// 파일이 존재하면
		if ( File.Exists(path) )
		{
			Logger.Log("Database::Read - File Exist in Folder");

			Stream			stream		= new FileStream(path, FileMode.Open);
			BinaryFormatter	serializer	= new BinaryFormatter();

			DBItem = (DatabaseItem)serializer.Deserialize(stream);

			stream.Close();
		}
		// 파일이 존재하지 않으면
		else
		{
			Logger.Log("Database::Read - File Not Exist in Folder");
			Reset();
		}

		IsReaded = false;
	}

	public static void Reset()
	{
		// DBItem이 null이면 메모리 할당
		if ( DBItem == null ) DBItem = new DatabaseItem();
		// 전체 데이터 초기화
		DBItem.Reset();
		// 파일에 초기화된 데이터 저장
		Write();
	}
}

[System.Serializable]
public class DatabaseItem
{
	public	DBItem_Player		player;
	public	DBItem_Goods		goods;
	public	DBItem_Chapter[]	chapters;

	public DatabaseItem()
	{
		player	 = new DBItem_Player();
		goods	 = new DBItem_Goods();
		chapters = new DBItem_Chapter[Database.maxChapter];
		for ( int i = 0; i < chapters.Length; ++i)
		{
			chapters[i] = new DBItem_Chapter();
		}
	}

	public void Reset()
	{
		player.Reset();
		goods.Reset();
		
		for ( int i = 0; i < chapters.Length; ++ i )
		{
			chapters[i].Reset();
		}
		// 첫 번째 챕터의 해금 여부를 true로 설정
		chapters[0].isUnlock = true;
	}
}

[System.Serializable]
public class DBItem_Player
{
	public	int		level;
	public	float	experience;

	public void Reset()
	{
		level		= 1;
		experience	= 0f;
	}
}

[System.Serializable]
public class DBItem_Goods
{
	public	int		heart;
	public	float	gem;

	public readonly	int maxHeart = 50;

	public void Reset()
	{
		heart	= maxHeart;
		gem		= 0;
	}
}

[System.Serializable]
public class DBItem_Chapter
{
	public	bool	isUnlock;	// 챕터 해금 여부
	public	int		bestStage;	// 현재 챕터에서 도달한 최고 스테이지

	public void Reset()
	{
		isUnlock	= false;
		bestStage	= 1;
	}
}

