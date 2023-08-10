"""
Copyright (c) Microsoft Corporation. All rights reserved.
Licensed under the MIT License.
"""


from teams.ai.planner.plan_type import PlanType
from teams.ai.planner.predicted_command import PredictedCommand

class Plan:
    type: PlanType
    commands: list[PredictedCommand]