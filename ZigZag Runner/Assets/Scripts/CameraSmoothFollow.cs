using UnityEngine;

public class CameraSmoothFollow : MonoBehaviour
{
    private GameObject _player;
    private Vector3 _offset;
    private Vector3 _currentVelocity = Vector3.zero;
    private float _smoothTime = 0.35f;
    private bool _gameOver = false;

    private void Awake()
    {
        _player = GameObject.Find("Player");
        if (_player == null)
        {
            Debug.LogError("CameraSmoothFollow.cs::Awake() - Player GameObject Not Found.");
        }

        _offset = transform.position - _player.transform.position;
    }

    private void LateUpdate()
    {
        if (_gameOver == false)
        {
            Vector3 targetPosition = _player.transform.position + _offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, _smoothTime);
        }
    }

    public void GameOver() {
        _gameOver = true;
    }
}
