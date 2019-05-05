using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class MonitorStatistic : MonoBehaviour
{
    public enum StatisticType
    {
        Health,
        Paddle,
        Food
    }

    [SerializeField] private StatisticType statisticType;

    private Text text;
    private string monitorEvent = null;

    void Start()
    {
        this.text = this.GetComponent<Text>();

        switch (this.statisticType)
        {
            case StatisticType.Health:
                this.monitorEvent = GameManager.HealthChangeEvent;
                break;

            case StatisticType.Paddle:
                this.monitorEvent = GameManager.PaddleChangeEvent;
                break;

            case StatisticType.Food:
                this.monitorEvent = GameManager.FoodChangeEvent;
                break;
        }

        Debug.Assert(this.monitorEvent != null, "No statistic type at start");
        if (this.monitorEvent != null)
        {
            EventSystem.Subscribe(this.monitorEvent, this.OnStatisticChanged);
            EventSystem.Subscribe(
                GameManager.GameReady,
                this.OnStatisticChanged);
            if (GameManager.Singleton != null)
                this.UpdateStatistic();
        }
    }

    void OnDestroy()
    {
        if (this.monitorEvent != null)
        {
            EventSystem.Unsubscribe(this.monitorEvent, this.OnStatisticChanged);
            EventSystem.Unsubscribe(
                GameManager.GameReady,
                this.OnStatisticChanged);
        }
    }

    private void OnStatisticChanged(object sender, EventArgs e)
    {
        this.UpdateStatistic();
    }

    private void UpdateStatistic()
    {
        var gm = GameManager.Singleton;
        Debug.Assert(gm != null, "Game manager is not available at update");
        if (gm != null)
        {
            string message = string.Empty;
            switch (this.statisticType)
            {
                case StatisticType.Health:
                    message = $"{gm.CurrentHealth} / {GameManager.MaxHealth}";
                    break;

                case StatisticType.Paddle:
                    message = $"{gm.CurrentPaddles} / {GameManager.MaxPaddle}";
                    break;

                case StatisticType.Food:
                    message = $"{gm.CurrentFood} / {GameManager.MaxFood}";
                    break;
            }

            this.text.text = message;
        }
    }
}
