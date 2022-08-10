using UnityEngine;
public class Spawner : MonoBehaviour
{
    [SerializeField]
    private float _interval = 3; // �����Ԋu
    [SerializeField]
    private float spawn_x,spawn_y,spawn_z;
    [SerializeField]
    private int spawnmax = 3;//���v�X�|�[���\��
    [SerializeField]
    private GameObject _original = null; // �X�|�[�����ɕ�������Q�[���I�u�W�F�N�g

    private GameObject enemy = null;

    private float _elapsed; // �o�ߎ���
    void Update()
    {
        _elapsed += Time.deltaTime;
        if (this.transform.childCount < spawnmax)//�q�I�u�W�F�N�g�̐�
        {
            if (_elapsed > _interval)
            {
                _elapsed = 0;
                enemy = Instantiate(_original);
                enemy.transform.position = new Vector3(spawn_x,spawn_y,spawn_z);
                //�����I�u�W�F�N�g�̓X�|�i�[�̎q�I�u�W�F�N�g��
                enemy.transform.parent = this.transform;
                var rigidbody = enemy.GetComponent<Rigidbody>();
            }
        }
    }
}