using UnityEngine;
public class Spawner : MonoBehaviour
{
    [SerializeField]
    private float _interval = 3; // 生成間隔
    [SerializeField]
    private float spawn_x,spawn_y,spawn_z;
    [SerializeField]
    private int spawnmax = 3;//合計スポーン可能数
    [SerializeField]
    private GameObject _original = null; // スポーン時に複製するゲームオブジェクト

    private GameObject enemy = null;

    bool _active;

    private float _elapsed; // 経過時間
    void Update()
    {
        if (!_active) return;
        _elapsed += Time.deltaTime;
        if (this.transform.childCount < spawnmax)//子オブジェクトの数
        {
            if (_elapsed > _interval)
            {
                _elapsed = 0;
                enemy = Instantiate(_original);
                enemy.transform.position = new Vector3(spawn_x,spawn_y,spawn_z);
                //複製オブジェクトはスポナーの子オブジェクトに
                enemy.transform.parent = this.transform;
                var rigidbody = enemy.GetComponent<Rigidbody>();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _active = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _active = false;
        }
    }
}